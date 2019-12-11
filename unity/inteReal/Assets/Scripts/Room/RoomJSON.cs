using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RoomJSON {
    // room의 json 구조를 그대로 가지고 있고, Room 객체를 만들것임
    public int size;
    public List<int[]> outerpoints;
    public List<List<int[]>> innerpoints;
    public float ceiling;
    public string roomname;
    public List<Door> door;
    public List<Product> product;

    public class Point {
        public int x;
        public int y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }

    public class Door {
        public int[] point_1;
        public int[] point_2;
        public int height_1;
        public int height_2;
    }

    public class Product {
        public string product_id;
        public float[] position;
        public float rotation;

        public Product (string product_id, float[] position, float rotation) {
            this.product_id = product_id;
            this.position = position;
            this.rotation = rotation;
        }
    }
}