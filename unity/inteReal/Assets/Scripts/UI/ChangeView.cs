using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if(curView == 1) {
            btn.GetComponent<Image>().sprite = view3;
            cam.transform.rotation = Quaternion.Euler(90, 0, 0);
            curView = 3;
            Debug.Log("VIEW CHANGED TO FIRST");
        }
        else {
            btn.GetComponent<Image>().sprite = view1;
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            curView = 1;
            Debug.Log("VIEW CHANGED TO THIRD");
        }
    }
}
