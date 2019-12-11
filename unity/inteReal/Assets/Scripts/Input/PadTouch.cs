using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTouch : MonoBehaviour
{
    public PadTouchWatcher watcher;
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        watcher.PadTouchPress.AddListener(onPadTouchEvent);
    }

    public void onPadTouchEvent(Vector2 pos) {
        float theta = Mathf.Atan2(pos.y, pos.x);
        float sign = Mathf.Sign(Mathf.Sin(angle));
        angle = sign * theta;
        // Debug.Log("Pad angle: " + angle);
    }
}
