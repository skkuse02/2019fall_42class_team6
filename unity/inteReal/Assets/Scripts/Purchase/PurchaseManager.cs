using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseManager : MonoBehaviour
{
    public GameObject purchasePrefab;
    public GameObject purchaseContent;
    
    public void AddPurchase(ProductionJSON product) {
        Debug.Log("Purchased! product: " + product.product_id);
        GameObject purchaseItem = MakePurchaseItem(product);
        purchaseItem.transform.parent = purchaseContent.transform;
        purchaseItem.transform.localScale = new Vector3(1.4f, 1, 1);
        purchaseItem.transform.localPosition = new Vector3(purchaseItem.transform.localPosition.x,
                                                           purchaseItem.transform.localPosition.y, 0);
    }

    public GameObject MakePurchaseItem(ProductionJSON product) {
        // make prefab into gameobject in list
        GameObject productItem = Instantiate(purchasePrefab, Vector3.zero, Quaternion.identity);

        productItem.transform.GetChild(2).GetComponent<Text>().text = product.product_name;
        productItem.transform.GetChild(3).GetComponent<Text>().text = product.price + "원";
        productItem.SetActive(true);
        return productItem;
    }
}
