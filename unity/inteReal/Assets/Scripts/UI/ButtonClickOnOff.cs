using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickOnOff : MonoBehaviour {
    public GameObject pan;
    private Button btn;
    private bool act = false;

    // Start is called before the first frame update
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(OnOff);
    }

    void OnOff() {
        if(act) {
            pan.SetActive(false);
            act = false;
        }
        else {
            pan.SetActive(true);
            act = true;
        }
    }
}
