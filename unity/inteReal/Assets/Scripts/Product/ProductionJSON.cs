using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductionJSON
{
    public string product_id;
    public string product_name;
    public string company;
    public float width;
    public float height;
    public float depth;
    public string color;
    public string category;
    public int price;
    public string descrip;

    public ProductionJSON(string id, string product_name, string category, string company, int price, 
                            float width, float height, float depth, string color, string descrip ="") {
        this.product_id = id;
        this.company = company;
        this.category = category;
        this.product_name = product_name;
        this.descrip = descrip;
        this.width = width;
        this.height = height;
        this.depth = depth;
        this.color = color;
        this.price = price;
    }
}
