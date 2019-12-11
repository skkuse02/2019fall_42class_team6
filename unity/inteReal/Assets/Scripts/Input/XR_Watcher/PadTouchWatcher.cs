using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class PadTouchEvent : UnityEvent<Vector2> { }

public class PadTouchWatcher : MonoBehaviour
{
    public PadTouchEvent PadTouchPress;

    private bool lastButtonState = false;
    private List<InputDevice> devicesWithPadTouch;

    private void Awake()
    {
        if (PadTouchPress == null)
        {
            PadTouchPress = new PadTouchEvent();
        }

        devicesWithPadTouch = new List<InputDevice>();
    }

    private void OnEnable()
    {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices)
            InputDevices_deviceConnected(device);

        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable()
    {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithPadTouch.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device)
    {
        Vector2 _trash;
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _trash))
        {
            devicesWithPadTouch.Add(device); // Add any devices that have a primary button.
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device)
    {
        if (devicesWithPadTouch.Contains(device))
            devicesWithPadTouch.Remove(device);
    }

    void Update()
    {
        bool tempState = false;
        Vector2 tempAxis = Vector2.zero;
        foreach (var device in devicesWithPadTouch)
        {
            Vector2 padTouchAxis = Vector2.zero;
            tempState = device.TryGetFeatureValue(CommonUsages.primary2DAxis, out padTouchAxis) // did get a value
                        && (padTouchAxis != Vector2.zero) // the value we got
                        || tempState; // cumulative result from other controllers
            tempAxis = padTouchAxis;
        }

        if (tempState != lastButtonState) // Button state changed since last frame
        {
            PadTouchPress.Invoke(tempAxis);
            lastButtonState = tempState;
        }
    }
}