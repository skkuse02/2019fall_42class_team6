using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class ChangeView : MonoBehaviour {
    public Camera cam;
    public Sprite view1;
    public Sprite view3;
    private Button btn;
    private int curView = 1;

    // Start is called before the first frame update
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(View);
    }

    void View() {
        if (XRDevice.isPresent) {

        }
        else {
            if (curView == 1) {
                btn.GetComponent<Image>().sprite = view3;
                cam.transform.rotation = Quaternion.Euler(90, 0, 0);
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + 8, cam.transform.position.z);
                curView = 3;
            }
            else {
                btn.GetComponent<Image>().sprite = view1;
                cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 8, cam.transform.position.z);
                curView = 1;
            }
        }
    }
}