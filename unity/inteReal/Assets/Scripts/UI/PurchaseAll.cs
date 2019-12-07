using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseAll : MonoBehaviour {
    private Button btn;
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(PurchaseAllProducts);
    }

    void PurchaseAllProducts() {
        Debug.Log("PURCHASE ALL");
    }
}
