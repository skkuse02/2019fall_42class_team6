using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Newtonsoft.Json;
using System.IO;

public class ProductionLoader : MonoBehaviour
{
    const string testJSONPath = "Assets/Resources/product_list.json";
    public GameObject prefab;
    public Transform viewport;

    public ProductionLoader(GameObject prefab, Transform viewport) {
        this.prefab = prefab;
        this.viewport = viewport;
    }

    public List<ProductionJSON> LoadTestProducts() {
        string json = GetTestJSON();
        return JsonConvert.DeserializeObject<List<ProductionJSON>>(json);
    }

    [MenuItem("Tools/Read file")]
    private static string GetTestJSON() {

        StreamReader reader = new StreamReader(testJSONPath);
        string json = reader.ReadToEnd();
        reader.Close();

        return json;
    }

    public void DrawProductionList(List<ProductionJSON> productions) {
        foreach (ProductionJSON product in productions) {
            GameObject productItem = Instantiate<GameObject>(prefab, viewport);
            //productItem -> set options
            productItem.transform.parent = viewport;
        }
    }
}
