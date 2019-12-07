using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool isMenuOn = false;
    public Canvas canvas;
    public GameObject player;

    void Start()
    {
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    public void onPrimaryButtonEvent(bool pressed) {
        if (pressed)    isMenuOn ^= true;
        StartCoroutine(ToggleMenuCanvas());
    }

    private IEnumerator ToggleMenuCanvas() {
        if (isMenuOn) {
            canvas.gameObject.SetActive(false);
        }
        else {
            canvas.gameObject.SetActive(true);
            canvas.transform.position = player.transform.position;
        }
        yield return null;
    }
}
