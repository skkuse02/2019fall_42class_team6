using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;

public class ModelLoader : MonoBehaviour
{
    const string testObjPath = "Assets/Resources/vase/01Alocasia_obj.obj";
    const string testTexPath = "Assets/Resources/vase/leaf.jpg";
    //const string testObjPath = "Assets/Resources/Furniture/product_1.obj";
    //const string testTexPath = "Assets/Resources/Furniture/product_1.png";

    public GameObject LoadTestModel() {
        return LoadModel(testObjPath, testTexPath);
    }

    public GameObject LoadModel(string objPath, string texPath) {
        Mesh mesh = new Mesh();
        GameObject loadedObj = new OBJLoader().Load(objPath);
        Texture2D texture = ImageLoader.LoadTexture(texPath);

        Transform child = loadedObj.transform.GetChild(0);
        GameObject childObj = child.gameObject;
        Rigidbody rb = childObj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        childObj.GetComponent<Renderer>().material.mainTexture = texture;
        return loadedObj;
    }
}
