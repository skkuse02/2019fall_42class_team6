using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Save : MonoBehaviour {
    private Button btn;
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(SaveGame);
    }

    void SaveGame() {
        Debug.Log("SAVE");
    }
}