using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.IO;

public class ProductManager : MonoBehaviour
{
    public LoginManager login;
    public HttpRequest http;
    public GameObject brandContent;
    public GameObject productPrefab;
    public GameObject shoppingPanelList;

    public ModelManager modelManager;

    public string curCategory = "";
    public string curBrand = "DEFAULT";
    //public List<ProductionJSON> products;

    bool updated;

    // Start is called before the first frame update
    void Start() {
        updated = false;
        curBrand = "DEFAULT";
    }

    public string GetProductsByKeyword(string keyword) {
        Dictionary<string, string> parameters = new Dictionary<string, string>();

        parameters.Add("function", "GetProductListByKeyword");
        parameters.Add("keyword_id", keyword);

        //StartCoroutine(http.Get("http://" + host + ":" + port + "/keyword", parameters));
        //new WaitUntil(() => http.last_text != null);

        //Debug.Log("GET received: " + http.last_text);
        //return http.last_text;

        //string text = null;
        //StartCoroutine(httpManager.GetRoutine("http://" + host + ":" + port + "/keyword", parameters, 
        //    (body) => {
        //        text = body.text;
        //    }));
        //return text;
        return http.Get("http://" + LoginManager.host + ":" + LoginManager.port + "/keyword", parameters);
    }

    public List<ProductionJSON> GetAllProductionsObj() {
        string json = GetProductsByKeyword("");

        Debug.Log(json);
        var products = JsonConvert.DeserializeObject<List<ProductionJSON>>(json);
        return products;
    }

    //public string GetProductionsByCategory(string category) {
    //    Dictionary<string, string> headerOpt = new Dictionary<string, string>();
    //    Dictionary<string, string> bodyOpt = new Dictionary<string, string>();

    //    headerOpt.Add("Content-Type", "application/x-www-form-urlencoded");
    //    headerOpt.Add("Accept", "application/json");
    //    bodyOpt.Add("function", "SearchByCategory");
    //    bodyOpt.Add("category", category);

    //    //Debug.Log("gpbc check: " + http.last_text);
    //    StartCoroutine(http.Post("http://" + host + ":" + port + "/product", headerOpt, bodyOpt, new Dictionary<string, string>()));

    //    //Debug.Log(http.last_text);
    //    return http.last_text;
    //}

    public GameObject GetActiveShoppingContent() {
        for (int i=0; i< shoppingPanelList.transform.childCount; i++) {
            GameObject panel = shoppingPanelList.transform.GetChild(i).gameObject;
            if (panel.activeSelf == true) {
                return panel;
            }
        }
        return null;
    }

    public void RenderProducts(List<ProductionJSON> products) {
        GameObject productContent = GetActiveShoppingContent().transform.GetChild(2).GetChild(0).GetChild(0).gameObject;

        StartCoroutine(ClearContent(productContent));

        Debug.Log("# of products: " + products.Count);
        foreach (ProductionJSON product in products) {
            Debug.Log("category: "+curCategory+"    brand: "+curBrand);
            if ((curCategory.Equals("") || product.category.Equals(curCategory)) &&
                (curBrand.Equals("DEFAULT") || product.company.Equals(curBrand))) {
                GameObject productItem = MakeProductionItem(product);
                productItem.transform.parent = productContent.transform;
                Debug.Log(productItem.transform.localPosition.z);
                productItem.transform.localScale = Vector3.one;
                productItem.transform.localPosition = new Vector3(productItem.transform.localPosition.x,
                                                          productItem.transform.localPosition.y, 0);
            }
        }
    }

    public void SetCategory(string category) {
        curCategory = category;
        RenderProducts(GetAllProductionsObj());
    }

    public void SetBrand(string brand) {
        curBrand = brand;
        RenderProducts(GetAllProductionsObj());
    }

    public void SetKeyword(string keyword) {
        string json = GetProductsByKeyword(keyword);
        var products = JsonConvert.DeserializeObject<List<ProductionJSON>>(json);
        Debug.Log("Searching keyword: " + keyword);
        RenderProducts(products);
    }

    //public void UpdateProducts() {
    //    string json = GetAllProductions();
    //    var temp = JsonConvert.DeserializeObject<List<ProductionJSON>>(json);
    //    //var tmp = JsonConvert.DeserializeObject<List<JsonArrayAttribute>>(json);
    //    //var temp = new List<ProductionJSON>(tmp);

    //    if (temp != null)
    //        products = temp;


    //}

    //public void RenderProducts() {
    //    BrandClick brand = GetActiveBrandClick();
    //    GameObject productContent = brand.productContent;

    //    ClearContent(productContent);

    //    Debug.Log("# of products: " + products.Count);
    //    foreach (ProductionJSON product in products) {
    //        Debug.Log(string.Format("category: {0}, product: {1} / brand: {2}, product: {3}", curCategory, product.category, curBrand, product.company));

    //        if ((curCategory.Equals("") || product.category.Equals(curCategory)) &&
    //            (curBrand.Equals("DEFAULT") || product.company.Equals(curBrand))) {
    //            GameObject productItem = MakeProductionItem(product);
    //            productItem.transform.parent = productContent.transform;
    //            productItem.transform.localScale = Vector3.one;
    //        }
    //    }
    //    Debug.Log(string.Format("Render products: category[{0}] - brand[{1}]", curCategory, curBrand));
    //}

    public GameObject MakeProductionItem(ProductionJSON product) {
        // make prefab into gameobject in list
        GameObject productItem = Instantiate(productPrefab, Vector3.zero, Quaternion.identity);

        productItem.transform.GetChild(1).GetComponent<AddCart>().product = product;
        productItem.transform.GetChild(2).GetComponent<PlaceObject>().product = product;
        productItem.transform.GetChild(3).GetComponent<Purchase>().product = product;

        productItem.transform.GetChild(5).GetComponent<Text>().text = product.product_name;
        productItem.transform.GetChild(6).GetComponentInChildren<Text>().text = product.descrip;
        productItem.transform.GetChild(7).GetComponent<Text>().text = product.price + "원";
        productItem.SetActive(true);
        return productItem;
    }

    public IEnumerator ClearContent(GameObject content) {
        for (int i = content.transform.childCount - 1; i >= 0; i--) {
            //Destroy(content.transform.GetChild(i).gameObject);
            content.transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0);
    }
}
