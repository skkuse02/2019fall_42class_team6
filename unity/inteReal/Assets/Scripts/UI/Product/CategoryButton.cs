using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
    private Button btn;

    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(GetProductionsByCategory);
    }

    void GetProductionsByCategory() {

    }
}
