using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCart : MonoBehaviour
{
    public CartManager manager;
    public ProductionJSON product;
    private Button btn;

    void Start()
    {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(AddToCart);
    }

    void AddToCart() {
        manager.AddToCart(product.product_id);
    }
}
