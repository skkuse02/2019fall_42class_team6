using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class RoomParser {
    // JSON을 가져와서 RoomJSON 객체로 change
    public RoomJSON Parse(string json) {
        //return JsonUtility.FromJson<RoomJSON> (json);
        return JsonConvert.DeserializeObject<RoomJSON> (json);
    }

    public Room Convert(RoomJSON roomJSON) {
        Room.Floor floor = new Room.Floor(roomJSON.size);
        List<Room.Wall> walls = new List<Room.Wall>();

        List<List<int[]>> wallLines = new List<List<int[]>>();
        wallLines.Add(roomJSON.outerpoints);
        wallLines.AddRange(roomJSON.innerpoints);

        List<Room.Door> windowsAndDoors = new List<Room.Door>();

        foreach (List<int[]> points in wallLines) {
            for (int i=0; i<points.Count-1; i++) {
                int[] p1 = points[i];
                int[] p2 = points[i+1];

                Vector3Int origin = new Vector3Int(Mathf.Min(p1[0], p2[0]), 0, Mathf.Min(p1[1], p2[1]));
                bool isVertical = (p1[0] == p2[0]);
                Room.Wall wall = new Room.Wall(origin, isVertical, 
                    Mathf.Max(Mathf.Abs(p2[0] - p1[0]), Mathf.Abs(p2[1] - p1[1])), 
                    Mathf.RoundToInt(roomJSON.ceiling * 100));

                foreach (RoomJSON.Door door in roomJSON.door) {
                    if (wall.isDoorOn(door)) {
                        //int x1 = door.point_1[0];
                        //int x2 = door.point_2[0];
                        //int z1 = door.point_1[1];
                        //int z2 = door.point_2[1];
                        //Debug.Log(string.Format("wall: [({0}, {1}), ({2}, {3})]\ndoor: [({4}, {5}), ({6}, {7})]", 
                        //    p1[0], p1[1], p2[0], p2[1], x1, z1, x2, z2));
                        if (door.height_1 == 0)     wall.addDoor(door);
                        else                        wall.addWindow(door);
                        break;
                    }
                }
                walls.Add(wall);
            }
        }

        return new Room(floor, walls);
    }

    //private int GetFloorSize(List<RoomJSON.Point> points) {
    //    RoomJSON.Point min_point = new RoomJSON.Point(int.MaxValue, int.MaxValue);
    //    RoomJSON.Point max_point = new RoomJSON.Point(int.MinValue, int.MinValue);
    //    foreach (RoomJSON.Point p in points) {
    //        if (p.x < min_point.x || p.y < min_point.y)     min_point = p;
    //        if (p.x > max_point.x || p.y > max_point.y)     max_point = p;
    //    }
    //    return Mathf.Max(max_point.x - min_point.x, max_point.y - min_point.y);
    //}
}