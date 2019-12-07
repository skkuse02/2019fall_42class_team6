using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {
    private float speed = 1.0f;
    private float rotation_speed = 50.0f;

    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            this.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A)) {
            this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S)) {
            this.transform.Translate(new Vector3(0, 0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D)) {
            this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.X)) {
            this.transform.Translate(new Vector3(0, speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Z)) {
            this.transform.Translate(new Vector3(0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Q)) {
            this.transform.Rotate(new Vector3(0, -rotation_speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.E)) {
            this.transform.Rotate(new Vector3(0, rotation_speed * Time.deltaTime, 0));
        }
    }
}
