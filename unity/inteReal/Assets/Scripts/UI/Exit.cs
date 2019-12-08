using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour {
    private Button btn;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(exitApplication);
    }

    void exitApplication() {
        Application.Quit();
    }
}