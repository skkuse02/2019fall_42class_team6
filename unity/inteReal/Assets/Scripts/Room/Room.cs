using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    public Floor floor;
    public List<Wall> walls;

    public Room(Floor floor, List<Wall> walls) {
        this.floor = floor;
        this.walls = walls;
    }

    public class Floor {
        public int size;
        public Material mat;

        public Floor(int size) {
            this.size = size;
        }
    }

    private List<Wall> SplitWall(Wall wall) {
        List<Hole> holes = new List<Hole>();
        holes.AddRange(wall.doors);
        holes.AddRange(wall.windows);
        holes.Sort((x, y) => x.GetOffset().CompareTo(y.GetOffset()));

        List<Wall> subWalls = new List<Wall>();
        int start = 0;
        foreach (Hole hole in holes) {
            int middle = hole.GetOffset();
            int end = middle + hole.width;
            //Debug.Log(string.Format("start: {0}, middle: {1}, end: {2}", start, middle, end));

            subWalls.Add(wall.GetSubWall(start, 0, middle - start, wall.height));
            if (hole is Window w) {
                Wall below = wall.GetSubWall(middle, 0, end - middle, w.offset.y);
                Wall above = wall.GetSubWall(middle, w.offset.y + w.height, end - middle, wall.height);
                subWalls.AddRange(SplitWall(below));
                subWalls.AddRange(SplitWall(above));
            }
            if (hole is Door d) {
                Wall above = wall.GetSubWall(middle, d.height, end - middle, wall.height);
                subWalls.AddRange(SplitWall(above));
            }

            start = end;
        }
        subWalls.Add(wall.GetSubWall(start, 0, wall.width, wall.height));

        //if (subWalls.Count == 0)    subWalls.Add(wall);
        return subWalls;
    }

    public GameObject GenerateRoom() {
        GameObject roomObj = new GameObject("Room");

        GameObject floorObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floorObj.name = "Floor";
        floorObj.transform.position = new Vector3(floor.size / 2.0f, 0, -floor.size / 2.0f) / 10;
        floorObj.transform.localScale = new Vector3(floor.size, 1, floor.size) / 10;
        floorObj.transform.parent = roomObj.transform;
        floorObj.transform.tag = "floor";
        floorObj.layer = 8;
        
        Rigidbody rb = floorObj.AddComponent<Rigidbody>();
        rb.mass = 1.0f;
        rb.isKinematic = true;

        List<Wall> subWalls = new List<Wall>();
        foreach (Wall wall in walls) {
            subWalls.AddRange(SplitWall(wall));
        }

        foreach (Wall wall in subWalls) {
            GameObject wallObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (wall.isVertical) {
                Vector3 pos = (wall.origin + new Vector3(0, wall.height / 2.0f, wall.width / 2.0f)) / 100;
                wallObj.transform.position = new Vector3(pos.x, pos.y, -pos.z);
                wallObj.transform.localScale = new Vector3(1, wall.height, wall.width) / 100;
            }
            else {
                Vector3 pos = (wall.origin + new Vector3(wall.width / 2.0f, wall.height / 2.0f, 0)) / 100;
                wallObj.transform.position = new Vector3(pos.x, pos.y, -pos.z);
                wallObj.transform.localScale = new Vector3(wall.width, wall.height, 1) / 100;
            }

            rb = wallObj.AddComponent<Rigidbody>();
            rb.mass = 1.0f;
            rb.isKinematic = true;
            wallObj.transform.parent = roomObj.transform;
            wallObj.transform.tag = "wall";
            wallObj.layer = 8;
        }

        return roomObj;
    }

    public class Wall {
        public Vector3Int origin;
        public bool isVertical;
        public int width;
        public int height;
        public int thickness;
        public Material mat;
        public List<Door> doors;
        public List<Window> windows;

        public Wall(Vector3Int origin, bool isVertical, int width, int height, int thickness=1) {
            this.origin = origin;
            this.isVertical = isVertical;
            this.width = width;
            this.height = height;
            this.thickness = thickness;
            doors = new List<Door>();
            windows = new List<Window>();
        }

        public Wall GetSubWall(int offX, int offY, int width, int height) {
            Vector3Int subOrigin = origin;
            
            if (isVertical) {
                subOrigin += new Vector3Int(0, offY, offX);
                width = Mathf.Min(subOrigin.z + width, origin.z + this.width) - subOrigin.z;
                height = Mathf.Min(subOrigin.y + height, origin.y + this.height) - subOrigin.y;
            }
            else {
                subOrigin += new Vector3Int(offX, offY, 0);
                width = Mathf.Min(subOrigin.x + width, origin.x + this.width) - subOrigin.x;
                height = Mathf.Min(subOrigin.y + height, origin.y + this.height) - subOrigin.y;
            }

            Wall subWall = new Wall(subOrigin, isVertical, width, height, thickness);

            int origX = isVertical ? subOrigin.z : subOrigin.x;
            foreach (Door d in doors) {
                if (origX < d.offset && d.offset + d.width < origX + width
                    && offY == 0 && d.height < height)
                    subWall.addDoor(d);
            }
            foreach (Window w in windows) {
                if (origX < w.offset.x && w.offset.x + w.width < origX + width
                    && offY < w.offset.y && offY + w.height < offY + height)
                    subWall.addWindow(w);
            }

            return subWall;
        }

        public bool isDoorOn(RoomJSON.Door door) {
            int x1 = door.point_1[0];
            int x2 = door.point_2[0];
            int z1 = door.point_1[1];
            int z2 = door.point_2[1];

            if (isVertical) {
                if (x1 != x2)           return false;
                if (x1 != origin.x)     return false;
                return origin.z < Mathf.Min(z1, z2)
                    && Mathf.Max(z1, z2) < origin.z + width;
            }
            else {
                if (z1 != z2)           return false;
                if (z1 != origin.z)     return false;
                return origin.x < Mathf.Min(x1, x2)
                    && Mathf.Max(x1, x2) < origin.x + width;
            }
        }

        public void addDoor(Door door) {
            doors.Add(door);
        }

        public void addDoor(RoomJSON.Door door) {
            int x1 = door.point_1[0];
            int x2 = door.point_2[0];
            int z1 = door.point_1[1];
            int z2 = door.point_2[1];

            int height = door.height_2 - door.height_1;
            int width;
            int offset;

            if (isVertical) {
                width = Mathf.Abs(z1 - z2);
                offset = Mathf.Min(z1, z2) - origin.z;
            }
            else {
                width = Mathf.Abs(x1 - x2);
                offset = Mathf.Min(x1, x2) - origin.x;
            }
            Door d = new Door(offset, width, height);
            addDoor(d);
        }

        public void addWindow(Window window) {
            windows.Add(window);
        }

        public void addWindow(RoomJSON.Door door) {
            int x1 = door.point_1[0];
            int x2 = door.point_2[0];
            int z1 = door.point_1[1];
            int z2 = door.point_2[1];

            int height = door.height_2 - door.height_1;
            int width;
            Vector2Int offset;

            if (isVertical) {
                width = Mathf.Abs(z1 - z2);
                offset = new Vector2Int(Mathf.Min(z1, z2) - origin.z, door.height_1);
            }
            else {
                width = Mathf.Abs(x1 - x2);
                offset = new Vector2Int(Mathf.Min(x1, x2) - origin.x, door.height_1);
            }
            Window w = new Window(offset, width, height);
            addWindow(w);
        }
    }

    abstract public class Hole {
        public int width;
        public int height;

        abstract public int GetOffset();
    }

    public class Door : Hole {
        public int offset;

        public Door(int offset, int width, int height) {
            this.offset = offset;
            this.width = width;
            this.height = height;
        }
        
        override public int GetOffset() {
            return offset;
        }
    }

    public class Window : Hole {
        public Vector2Int offset;

        public Window(Vector2Int offset, int width, int height) {
            this.offset = offset;
            this.width = width;
            this.height = height;
        }

        override public int GetOffset() {
            return offset.x;
        }
    }
}
