﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class CartManager : MonoBehaviour
{
    const string username = "user1";
    const string password = "user1";
    string host = "34.66.144.16";
    string port = "3000";

    public HttpRequest http;
    public ProductManager productManager;
    public GameObject cartContent;
    public GameObject cartPrefab;

    void Start() {
    }

    public IEnumerator ClearContent(GameObject content) {
        for (int i = content.transform.childCount - 1; i >= 0; i--) {
            //Destroy(content.transform.GetChild(i).gameObject);
            content.transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1);
    }

    public void RenderCart() {
        StartCoroutine(ClearContent(cartContent));

        GetCartList(GetCartId(username));
    }

    public void AddToCart(string product) {
        string cart_id = GetCartId(username);

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "AddProductToCart");
        parameters.Add("cart_id", cart_id);
        parameters.Add("user_id", username);
        parameters.Add("product_id", product);

        http.Get("http://" + host + ":" + port + "/cart", parameters);

        RenderCart();
    }

    public void RemoveFromCart(string product) {
        string cart_id = GetCartId(username);

        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "RemoveProductFromCart");
        parameters.Add("cart_id", cart_id);
        parameters.Add("product_id", product);

        http.Get("http://" + host + ":" + port + "/cart", parameters);

        RenderCart();
    }

    public void GetCartList(string cart_id) {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetProductListByCartid");
        parameters.Add("cart_id", cart_id);

        //StartCoroutine(http.Get("http://" + host + ":" + port + "/cart", parameters));
        //new WaitForSeconds(1f);
        //string json = http.last_text;

        //string json = null;
        //StartCoroutine(httpManager.GetRoutine("http://" + host + ":" + port + "/keyword", parameters, 
        //    (body) => {
        //        Debug.Log("callback: " + body.text);
        //        json = body.text;
        //    }));

        string json = http.Get("http://" + host + ":" + port + "/cart", parameters);

        Debug.Log("CART:" + json);
        List<ProductIDJSON> cartList = JsonConvert.DeserializeObject<List<ProductIDJSON>>(json);
        List<ProductionJSON> products = productManager.GetAllProductionsObj();

        List<string> cartIDs = new List<string>();
        foreach (var item in cartList) {
            cartIDs.Add(item.product_id);
        }
        Debug.Log("CartIDs: " + string.Join(", ", cartIDs));

        foreach (ProductionJSON product in products) {
            if (cartIDs.Contains(product.product_id)) {
                GameObject cartItem = MakeCartItem(product);
                cartItem.transform.parent = cartContent.transform;
                cartItem.transform.localScale = new Vector3(1.4f, 1, 1);
            }
        }
        // need to change
    }

    public string GetCartId(string username) {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetCartid");
        parameters.Add("user_id", username);

        string json = http.Get("http://" + host + ":" + port + "/cart", parameters);

        List<CartIDJSON> cart_ids = JsonConvert.DeserializeObject<List<CartIDJSON>>(json);
        string cart_id = cart_ids[0].cart_id;
        return cart_id;
    }

    public GameObject MakeCartItem(ProductionJSON product) {
        GameObject cartItem = Instantiate(cartPrefab, Vector3.zero, Quaternion.identity);

        cartItem.transform.GetChild(2).GetComponent<Text>().text = product.product_name;
        cartItem.transform.GetChild(3).GetComponent<Text>().text = product.price + "원";

        cartItem.transform.GetChild(5).GetComponent<RemoveCart>().product = product;
        cartItem.SetActive(true);

        return cartItem;
    }
}