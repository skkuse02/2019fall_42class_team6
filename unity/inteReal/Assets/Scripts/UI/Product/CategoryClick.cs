using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryClick : MonoBehaviour {
    private Button btn;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(CheckCategory);
    }

    // Update is called once per frame
    void CheckCategory() {
        string category = btn.GetComponentInChildren<Text>().text;
        Debug.Log("category: " + category);
    }
}
