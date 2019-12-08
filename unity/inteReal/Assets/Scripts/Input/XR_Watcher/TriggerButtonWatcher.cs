using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

[System.Serializable]
public class TriggerButtonEvent : UnityEvent<bool> { }

public class TriggerButtonWatcher : MonoBehaviour
{
    public TriggerButtonEvent triggerButtonPress;

    private bool lastButtonState = false;
    private List<InputDevice> devicesWithTriggerButton;

    private void Awake() {
        if (triggerButtonPress == null) {
            triggerButtonPress = new TriggerButtonEvent();
        }
        devicesWithTriggerButton = new List<InputDevice>();
    }

    private void OnEnable() {
        List<InputDevice> allDevices = new List<InputDevice>();
        InputDevices.GetDevices(allDevices);
        foreach (InputDevice device in allDevices) {
            InputDevices_deviceConnected(device);
        }
        InputDevices.deviceConnected += InputDevices_deviceConnected;
        InputDevices.deviceDisconnected += InputDevices_deviceDisconnected;
    }

    private void OnDisable() {
        InputDevices.deviceConnected -= InputDevices_deviceConnected;
        InputDevices.deviceDisconnected -= InputDevices_deviceDisconnected;
        devicesWithTriggerButton.Clear();
    }

    private void InputDevices_deviceConnected(InputDevice device) {
        bool _trash;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out _trash)) {
            devicesWithTriggerButton.Add(device);
        }
    }

    private void InputDevices_deviceDisconnected(InputDevice device) {
        if (devicesWithTriggerButton.Contains(device)) {
            devicesWithTriggerButton.Remove(device);
        }
    }

    void Update() {
        bool tempState = false;
        foreach (var device in devicesWithTriggerButton) {
            bool triggerButtonState = false;
            tempState = device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonState)
                        && triggerButtonState
                        || tempState;
        }

        if (tempState != lastButtonState) {
            triggerButtonPress.Invoke(tempState);
            lastButtonState = tempState;
        }
    }
}
