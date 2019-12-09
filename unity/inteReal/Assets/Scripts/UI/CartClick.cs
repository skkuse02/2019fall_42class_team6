using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartClick : MonoBehaviour
{
    public CartManager manager;
    private Button btn;

    // Start is called before the first frame update
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Cart);
    }

    void Cart() {
        manager.RenderCart();
    }
}
