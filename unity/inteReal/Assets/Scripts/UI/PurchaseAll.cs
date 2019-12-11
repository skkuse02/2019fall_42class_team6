using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseAll : MonoBehaviour {
    private Button btn;
    public PurchaseManager purchaseManager;
    public CartManager cartManager;
    public LoginManager login;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(PurchaseAllProducts);
    }

    void PurchaseAllProducts() {
        List<ProductionJSON> cartList = cartManager.GetCartList(cartManager.GetCartId(login.user_id));

        foreach (var item in cartList) {
            purchaseManager.AddPurchase(item);
            cartManager.RemoveFromCart(item.product_id);
        }
    }
}
