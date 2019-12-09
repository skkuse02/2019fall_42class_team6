using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveCart : MonoBehaviour {
    public CartManager manager;
    public ProductionJSON product;
    private Button btn;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(RemoveFromCart);
    }

    void RemoveFromCart() {
        manager.RemoveFromCart(product.product_id);
    }
}
