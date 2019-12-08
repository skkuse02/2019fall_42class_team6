using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryClick : MonoBehaviour {
    private Button btn;
    public ProductManager productManager;
    public GameObject brandContents;
    public ProductManager manager;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(SetCategory);
    }

    void SetCategory() {
        string category = btn.GetComponentInChildren<Text>().text;
        manager.SetCategory(category);
        Debug.Log("Select category: " + category);
    }   
}