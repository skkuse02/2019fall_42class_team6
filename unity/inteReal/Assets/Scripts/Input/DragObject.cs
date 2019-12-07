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

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);


        if (target != null) {
            RaycastHit[] hits;
            bool onHit = false;

            hits = Physics.SphereCastAll(ray, 0.001f, 100.0f);

            foreach (RaycastHit hit in hits) {
                if (hit.collider.gameObject.CompareTag("wall") || hit.collider.gameObject.CompareTag("floor")) {

                    target.transform.position = hit.point;
                    Debug.DrawLine(ray.origin, hit.point);
                    Debug.Log(hit.point);

                    if (Input.GetMouseButtonDown(0)) {
                        Detach();
                    }
                    break;
                }
            }
        }
        else {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                GameObject target = hit.collider.gameObject;
                Debug.Log(target);
                if (Input.GetMouseButtonDown(0) && !target.CompareTag("wall") && !target.CompareTag("floor")) {
                    Attach(target);
                }
            }
        }

        //LayerMask mask = LayerMask.GetMask("Walls");
        //if (Physics.Raycast(ray, out hit, mask)) {
        //    if (target != null) {
        //        target.transform.position = hit.point;
        //        Debug.DrawLine(ray.origin, hit.point);
        //        Debug.Log(hit.point);

        //        if (Input.GetMouseButtonDown(0)) {
        //            Detach();
        //        }
        //    }
        //    else {
        //        GameObject target = hit.collider.gameObject;
        //        Debug.Log(target);
        //        if (Input.GetMouseButtonDown(0) && !target.CompareTag("wall") && !target.CompareTag("floor")) {
        //            Attach(target);
        //        }
        //    }
        //}
    }

    public void Detach() {
        //isAttached = false;
        target = null;
    }
}
