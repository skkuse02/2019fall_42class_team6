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
        Dictionary<string, string> param = new Dictionary<string, string>();
        param.Add("category", category);
        string endpoint = "64.66.144.16:3000/product";
        HttpRequest.Get(endpoint, param);
    }
}
