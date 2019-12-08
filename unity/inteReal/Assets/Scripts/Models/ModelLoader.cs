using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System.IO;

public class ModelLoader : MonoBehaviour
{
    const string testObjPath = "Assets/Resources/Chair.obj";
    const string testTexPath = "Assets/Resources/Chair.png";    // dummy, no image file
    // const string testObjPath = "Assets/Resources/vase/01Alocasia_obj.obj";
    // const string testTexPath = "Assets/Resources/vase/leaf.jpg";
    // const string testObjPath = "Assets/Resources/Furniture/product_1.obj";
    // const string testTexPath = "Assets/Resources/Furniture/product_1.png";

    public GameObject LoadTestModel() {
        return LoadModel(testObjPath, testTexPath);
    }

    public GameObject LoadModel(string objPath, string texPath) {
        Mesh mesh = new Mesh();
        GameObject loadedObj = new OBJLoader().Load(objPath);
        Texture2D texture = ImageLoader.LoadTexture(texPath);

        Transform child = loadedObj.transform.GetChild(0);
        GameObject childObj = child.gameObject;
        FitCollider(childObj);

        loadedObj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        Rigidbody rb = childObj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        childObj.GetComponent<Renderer>().material.mainTexture = texture;
        return loadedObj;
    }

    public GameObject LoadModelFromDir(string product_id) {
        string dir = Path.Combine("models", product_id);

        GameObject obj = new OBJLoader().Load(Path.Combine(dir, product_id + ".obj"));

        MTLLoader mtl = new MTLLoader();
        Dictionary<string, Material> textures = mtl.Load(Path.Combine(dir, product_id + ".mtl"));

        FitCollider(obj);
        obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        Rigidbody rb = obj.AddComponent<Rigidbody>();
        rb.isKinematic = true;

        foreach (var p in textures) {
            obj.GetComponent<Renderer>().material.SetTexture(p.Key, p.Value.mainTexture);
        }

        return obj;

        //for (int i=0; i<obj.transform.childCount; i++) {
        //    GameObject child = obj.transform.GetChild(i);
        //    child.GetComponent<Renderer>().material.
        //}
    }

    public void FitCollider(GameObject obj) {
        BoxCollider collider = obj.GetComponent<BoxCollider>();
        if (collider == null) {
            collider = obj.AddComponent<BoxCollider>();
        }

        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        bool hasBounds = false;
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers) {
            if(hasBounds) {
                bounds.Encapsulate(renderer.bounds);
            }
            else {
                bounds = renderer.bounds;
                hasBounds = true;
            }
        }
        if(hasBounds) {
            collider.center = bounds.center - obj.transform.position;
            collider.size = bounds.size;
        }
        else {
            collider.size = collider.center = Vector3.zero;
        }
    }
}
