using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using HTC.UnityPlugin.Vive;

public class RoomManager : MonoBehaviour
{
    public LoginManager login;
    private HttpRequest http;
    private RoomParser roomParser;
    public enum TeleportButton
    {
        Trigger,
        Pad,
        Grip,
    }

    public GameObject player;
    public Transform target;
    public Transform pivot;
    public float fadeDuration = 0.3f;

    private RoomJSON roomJSON;
    private bool loaded;

    void Start() {
        //btn = this.transform.GetComponent<Button>();
        //btn.onClick.AddListener(LoadRoom);

        http = new HttpRequest();
        roomParser = new RoomParser();
        loaded = false;

        //LoadRoom();
    }

    void Update() {
        if (login != null && login.loaded && !loaded) {
            LoadRoom();
            loaded = true;
        }
    }

    void LoadRoom() {
        Debug.Log("Room is Loading...");

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetRoomInfofile");
        parameters.Add("model_id", login.model_id);

        string json = http.Get("http://" + LoginManager.host + ":" + LoginManager.port + "/model", parameters);

        Debug.Log("new room: " + json);
        Debug.Log("old room: " + http.GetTestJSON());

        //string json = httpRequest.GetTestJSON();

        roomJSON = roomParser.Parse(json);
        Room room = roomParser.Convert(roomJSON);
        GameObject roomObj = room.GenerateRoom();

        GameObject[] floors = GameObject.FindGameObjectsWithTag("floor");
        foreach (GameObject fo in floors) {
            fo.AddComponent<Teleportable>();
            fo.AddComponent<Teleportable>().target = target;
            fo.AddComponent<Teleportable>().pivot = pivot;
            fo.AddComponent<Teleportable>().fadeDuration = fadeDuration;
        }

        ModelLoader loader = new ModelLoader();

        foreach (RoomJSON.Product p in roomJSON.product) {
            GameObject obj = loader.LoadModelFromDir(p.product_id);
            obj.transform.position = new Vector3(p.position[0], p.position[1], p.position[2]);
            obj.transform.Rotate(new Vector3(0, p.rotation, 0));
            Debug.Log("Load product: " + p.product_id);
        }

        MoveCamera(room);
    }

    public void SaveRoom() {
        GameObject products = GameObject.Find("Products");
        List<RoomJSON.Product> productList = new List<RoomJSON.Product>();

        if (products != null) {
            for (int i=0; i < products.transform.childCount; i++) {
                Transform child = products.transform.GetChild(i);
                string product_id = child.gameObject.GetComponent<Text>().text;
                float[] position = {child.position.x, child.position.y, child.position.z};
                float rotation = child.rotation.y;

                RoomJSON.Product product = new RoomJSON.Product(product_id, position, rotation);
                productList.Add(product);
            }
        }

        roomJSON.product = productList;
        string json = JsonConvert.SerializeObject(roomJSON);
        Debug.Log("Saving room: " + json);

        http.UploadJSON(login.model_id + "_roomInfo.json", json, "http://" + LoginManager.host + ":" + LoginManager.port + "/upload", new Dictionary<string, string>());

        //Dictionary<HttpRequestHeader, string> headerOpt = new Dictionary<HttpRequestHeader, string>();
        //Dictionary<string, string> bodyOpt = new Dictionary<string, string>();

        //headerOpt.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
        //headerOpt.Add(HttpRequestHeader.Accept, "application/json");
        //bodyOpt.Add("file", json);
        //bodyOpt.Add("password", password);

        //http.Post("http://" + host + ":" + port + "/upload", headerOpt, bodyOpt, new Dictionary<string, string>());
    }

    void MoveCamera(Room room) {

        float maxX = int.MinValue;
        float maxZ = int.MinValue;

        foreach (Room.Wall wall in room.walls) {
            if (wall.isVertical) {
                maxZ = Mathf.Max(maxZ, wall.origin.z + wall.width);
            }
            else {
                maxX = Mathf.Max(maxX, wall.origin.x + wall.width);
            }
        }

        player.transform.position = new Vector3(maxX / 200.0f, 2, -maxZ / 200.0f);
        Debug.Log("transform: " + player.transform.position.x);
    }
}
