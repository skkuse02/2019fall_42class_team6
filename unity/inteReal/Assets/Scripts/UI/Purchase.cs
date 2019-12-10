using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Purchase : MonoBehaviour
{
    private Button btn;
    public ProductionJSON product;
    public PurchaseManager purchaseManager;
    public CartManager cartManager;
    public LoginManager login;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(PurchaseProduct);
    }

    void PurchaseProduct() {
        purchaseManager.AddPurchase(product);
    }
}
