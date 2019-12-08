using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldTrigger : MonoBehaviour
{
    public TriggerButtonWatcher watcher;
    public bool isHold = false;

    void Start()
    {
        watcher.triggerButtonPress.AddListener(onTriggerButtonEvent);
    }

    public void onTriggerButtonEvent(bool pressed) {
        isHold = pressed;
        StartCoroutine(HoldTriggerButton());
    }

    private IEnumerator HoldTriggerButton() {
        if (isHold) {
            Debug.Log("trigger button PRESSED");
        }
        else {
            Debug.Log("trigger button RELEASED");
        }
        yield return null;
    }
}
