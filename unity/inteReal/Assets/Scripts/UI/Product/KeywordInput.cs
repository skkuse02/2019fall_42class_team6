using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeywordInput : MonoBehaviour {
    public InputField input;
    private Button btn;

    //// Start is called before the first frame update
    void Start() {
        btn = this.transform.GetComponent<Button>();
        btn.onClick.AddListener(Keyword);
    }

    private void Keyword() {
        string keyword = input.text;
        input.text = "";
        Debug.Log("text: " + keyword);
    }
}
