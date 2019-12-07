using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    //public Button btn;
    private HttpRequest httpRequest;
    private RoomParser roomParser;
    public GameObject player;

    void Start() {
        //btn = this.transform.GetComponent<Button>();
        //btn.onClick.AddListener(LoadRoom);

        httpRequest = new HttpRequest();
        roomParser = new RoomParser();

        LoadRoom();
    }

    void LoadRoom() {
        Debug.Log("Room is Loading...");

        Dictionary<string, string> parameters = new Dictionary<string, string>();
        string json = HttpRequest.GetTestJSON(); //httpRequest.Get("", parameters);

        Room room = roomParser.Convert(roomParser.Parse(json));
        GameObject roomObj = room.GenerateRoom();

        MoveCamera(room);
    }

    void MoveCamera(Room room) {

        int maxX = int.MinValue;
        int maxZ = int.MinValue;

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
