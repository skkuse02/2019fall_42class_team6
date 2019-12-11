using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelFileJSON
{
    public string product_file;

    public ModelFileJSON(string file) {
        this.product_file = file;
    }
}
