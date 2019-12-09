using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using HTC.UnityPlugin.Pointer3D;
using HTC.UnityPlugin.Utility;
using HTC.UnityPlugin.Vive;

public class DragObject : MonoBehaviour
{
    public Camera camera;
    public ViveRaycaster viveRay;
    public GameObject target;
    public HoldTrigger holdTrigger;

    public void Attach(GameObject obj) {
        target = obj;
    }

    public void Detach() {
        target = null;
    }

    void Update() {
        if(XRDevice.isPresent) {
            DragByVRController();
        }
        else {
            DragByMouse();
        }
    }

    void DragByMouse() {

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (target != null) {
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.001f, 100.0f);

            foreach (RaycastHit hit in hits) {
                if(hit.collider.gameObject.CompareTag("wall") || hit.collider.gameObject.CompareTag("floor")) {
                    target.transform.position = hit.point;

                    //Transform child = target.transform.GetChild(0);
                    //Vector3 diff = child.position - 
                    //Debug.Log("localscale: " + collider.transform.localPosition);
                    //target.transform.position = hit.point - collider.transform.localPosition;
                    //Debug.DrawLine(ray.origin, hit.point);

                    if (Input.GetMouseButtonDown(0)) {
                        Detach();
                    }
                    break;
                }
            }
        }
        else {
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                GameObject target = hit.collider.gameObject;
                //Debug.Log(target);
                if(Input.GetMouseButtonDown(0) && !target.CompareTag("wall") && !target.CompareTag("floor")) {
                    Attach(target);
                }
            }
        }
    }

    void DragByVRController() {
        Transform viveTransform = viveRay.transform;
        Vector3 forward = viveTransform.forward;
        Vector3 origin = viveTransform.position;

        if (target != null) {
            RaycastHit[] hits = Physics.SphereCastAll(origin, 0.001f, forward, 100.0f);

            foreach (RaycastHit hit in hits) {
                if(hit.collider.gameObject.CompareTag("wall") || hit.collider.gameObject.CompareTag("floor")) {
                    target.transform.position = hit.point;

                    //BoxCollider collider = target.GetComponent<BoxCollider>();
                    //target.transform.position = hit.point - collider.transform.localPosition;

                    if (!holdTrigger.isHold) {
                        Detach();
                    }
                    break;
                }
            }
        }
        else {
            RaycastHit hit;
            if(Physics.Raycast(origin, forward, out hit, 100.0f)) {
                GameObject target = hit.collider.gameObject;
                //Debug.Log(target);
                if(holdTrigger.isHold && !target.CompareTag("wall") && !target.CompareTag("floor")) {
                    Attach(target);
                }
            }
        }
    }
}
