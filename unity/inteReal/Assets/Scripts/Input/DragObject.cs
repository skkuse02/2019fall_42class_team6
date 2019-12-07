using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public Camera camera;
    public GameObject target;

    //bool isAttached = false;

    public void Attach(GameObject obj) {
        //isAttached = true;
        target = obj;
    }

    void Update() {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            if (target != null) {
                target.transform.position = hit.point;
                Debug.DrawLine(ray.origin, hit.point);
                Debug.Log(hit.point);

                if (Input.GetMouseButtonDown(0)) {
                    Detach();
                }
            }
            else {
                GameObject target = hit.transform.gameObject;
                Debug.Log(target);
                if (Input.GetMouseButtonDown(0) && !target.CompareTag("wall") && !target.CompareTag("floor")) {
                    Attach(target);
                }
            }
        }
    }

    public void Detach() {
        //isAttached = false;
        target = null;
    }
}
