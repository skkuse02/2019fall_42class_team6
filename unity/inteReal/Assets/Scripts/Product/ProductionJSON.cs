using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProductionJSON
{
    public List<Product> products;

    public class Product {
        public int product_id;
        public string brand;
        public string name;
        public string description;
        public int price;

        public Product(int id, string name, string brand, int price, string description ="") {
            this.product_id = id;
            this.brand = brand;
            this.name = name;
            this.description = description;
            this.price = price;
        }
    }
}
