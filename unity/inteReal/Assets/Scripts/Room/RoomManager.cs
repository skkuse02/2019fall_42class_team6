using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HTC.UnityPlugin.Vive;

public class RoomManager : MonoBehaviour
{
    const string username = "user1";
    const string password = "user1";
    string host = "34.66.144.16";
    string port = "3000";

    //public Button btn;
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

    bool loaded = false;

    void Start() {
        //btn = this.transform.GetComponent<Button>();
        //btn.onClick.AddListener(LoadRoom);

        http = new HttpRequest();
        roomParser = new RoomParser();

        //LoadRoom();
    }

    void Update() {
        if (!loaded) {
            LoadRoom();
            loaded = true;
        }
    }

    void LoadRoom() {
        Debug.Log("Room is Loading...");

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetRoomInfofile");
        parameters.Add("model_id", "model_9");

        string json = http.Get("http://" + host + ":" + port + "/model", parameters);

        Debug.Log("new room: " + json);
        Debug.Log("old room: " + http.GetTestJSON());

        //string json = httpRequest.GetTestJSON();

        Room room = roomParser.Convert(roomParser.Parse(json));
        GameObject roomObj = room.GenerateRoom();

        GameObject[] floors = GameObject.FindGameObjectsWithTag("floor");
        foreach (GameObject fo in floors) {
            fo.AddComponent<Teleportable>();
            fo.AddComponent<Teleportable>().target = target;
            fo.AddComponent<Teleportable>().pivot = pivot;
            fo.AddComponent<Teleportable>().fadeDuration = fadeDuration;
        }

        MoveCamera(room);
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
