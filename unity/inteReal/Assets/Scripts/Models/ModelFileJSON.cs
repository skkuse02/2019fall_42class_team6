using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelFileJSON : MonoBehaviour
{
    public string product_file;

    public ModelFileJSON(string file) {
        this.product_file = file;
    }
}
