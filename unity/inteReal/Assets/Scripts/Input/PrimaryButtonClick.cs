using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryButtonClick : MonoBehaviour
{
    public PrimaryButtonWatcher watcher;
    public bool isClicked = false;
    
    // Start is called before the first frame update
    void Start()
    {
        watcher.primaryButtonPress.AddListener(onPrimaryButtonEvent);
    }

    public void onPrimaryButtonEvent(bool pressed)
    {
        if (pressed) isClicked = true;
    }
}
