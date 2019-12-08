using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    private Button btn;
    public GameObject player;
    public DragObject dragger;
    public ProductionJSON product;
    public ModelManager manager;

    void Start()
    {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Place);
    }

    void Place() {
        manager.GetAllModelFiles(product.product_id);

        ModelLoader loader = new ModelLoader();

        //GameObject obj = loader.LoadTestModel();
        GameObject obj = loader.LoadModelFromDir(product.product_id);
        // obj.transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
        Debug.Log("placing start");
        dragger.Attach(obj);

        //obj.transform.position = player.transform.position;
    }
}
