using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    public GameObject player;
    private Button btn;
    public DragObject dragger;

    void Start()
    {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Place);
    }

    void Place() {
        ModelLoader loader = new ModelLoader();

        GameObject obj = loader.LoadTestModel();
        //obj.transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
        dragger.Attach(obj);

        //obj.transform.position = player.transform.position;
    }
}
