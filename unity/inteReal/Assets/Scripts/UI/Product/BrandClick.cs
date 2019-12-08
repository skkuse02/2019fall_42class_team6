using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrandClick : MonoBehaviour {
    public GameObject shoppingPanels;
    public GameObject itemPrefab;
    public GameObject productContent;
    public ProductManager manager;

    private List<GameObject> shoppingPanelList = new List<GameObject>();
    private Button btn;
    private int size;

    // Start is called before the first frame update
    void Start() {
        size = shoppingPanels.transform.childCount;
        for(int i=0; i<size; i++) {
            shoppingPanelList.Add(shoppingPanels.transform.GetChild(i).gameObject);
        }
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Brand);
    }
    
    void Brand() {
        int btnNum = (int)char.GetNumericValue(btn.name[btn.name.Length - 1]);

        for (int i = 1; i <= size; i++) {
            if (i == btnNum) {
                shoppingPanelList[i - 1].SetActive(true);
                string brandName = GetBrandName();
                Debug.Log("Brand: " + brandName);
                continue;
            }
            shoppingPanelList[i-1].SetActive(false);
        }

        manager.SetBrand(GetBrandName());

        //ProductionLoader productionLoader = new ProductionLoader(itemPrefab, productContent.transform);
        //productionLoader.DrawProductionList(productionLoader.LoadTestProducts());
    }

    public string GetBrandName() {
        return btn.GetComponentInChildren<Text>().text;
    }
}