using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveAction : MonoBehaviour, BasicInput.IPlayerActions
{
    private BasicInput controls;

    //public Camera cam;
    public float rotateSpeed = 0.3f;

    private float pitch = 0.0f;
    private float yaw = 0.0f;

    public void OnLook(InputAction.CallbackContext context) {
        //Debug.Log("Look: " + context.ReadValue<Vector2>());
        //Vector2 motion = context.ReadValue<Vector2>();
        //yaw += rotateSpeed * motion.x;
        //pitch -= rotateSpeed * motion.y;
        ////cam.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        //this.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    public void OnMove(InputAction.CallbackContext context) {
        //Debug.Log("Move: " + context.ReadValue<Vector2>());
        Vector3 direction = context.ReadValue<Vector2>();
        Rigidbody rb = this.GetComponent<Rigidbody>();
        this.transform.position += new Vector3(direction.x, 0.0f, direction.y);
        //rb.AddForce(new Vector3(direction.x, 0, direction.y));
        //this.transform.Translate(new Vector3(direction.x, 0, direction.y));
    }

    void Awake() {
        controls = new BasicInput();
        controls.Player.SetCallbacks(this);
    }

    void OnEnable() {
        controls.Player.Enable();
    }
    void OnDisable() {
        controls.Player.Disable();
    }
}
