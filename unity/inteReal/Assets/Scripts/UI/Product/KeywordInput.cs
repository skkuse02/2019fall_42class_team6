using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeywordInput : MonoBehaviour {
    public InputField input;
    public ProductManager manager;
    private Button btn;

    //// Start is called before the first frame update
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Keyword);
    }

    private void Keyword() {
        manager.SetKeyword(this.gameObject.GetComponent<InputField>().text);
    }
}
