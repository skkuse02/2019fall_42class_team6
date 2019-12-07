using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputKeyboard : MonoBehaviour {
    private InputField inputfield;
    public GameObject keyboardScreen;
    void Start() {
        inputfield = this.GetComponent<InputField>();
    }

    void Update() {
        if(inputfield.isFocused) {
            keyboardScreen.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        keyboardScreen.SetActive(true);
    }
}
