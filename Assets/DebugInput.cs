using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DebugInput : MonoBehaviour {

    public bool _enabled;

    public bool leftGrab;
    public bool rightGrab;
    public bool buttonOne;

    private OVRInput.OVRControllerBase left;

    private OVRInput.OVRControllerBase right;

    private OVRInput.OVRControllerBase touch;

    void Start() {
        Type inType = typeof(OVRInput);
        FieldInfo ctrlField = inType.GetField("controllers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        List<OVRInput.OVRControllerBase> controllers = (List<OVRInput.OVRControllerBase>) ctrlField.GetValue(null);
        controllers.ForEach(c => {
            if (c.controllerType == OVRInput.Controller.LTouch) {
                left = c;
            } else if (c.controllerType == OVRInput.Controller.RTouch) {
                right = c;
            } else if (c.controllerType == OVRInput.Controller.Touch) {
                touch = c;
            }
        });
    }

    void Update() {
        OVRInput.debugMode = _enabled;

        if (_enabled) {
            Type inType = typeof(OVRInput);
            FieldInfo activeCtrlField = inType.GetField("activeControllerType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            activeCtrlField.SetValue(null, OVRInput.Controller.Touch);
        }

        // Update previous states
        left.previousState = left.currentState;
        right.previousState = right.currentState;
        touch.previousState = touch.currentState;

        // Reset current state
        left.currentState.Buttons = 0;
        right.currentState.Buttons = 0;
        touch.currentState.Buttons = 0;

        Tuple<bool, OVRInput.OVRControllerBase, OVRInput.Button>[] toCheck = {
            Tuple.Create(leftGrab, left, OVRInput.Button.PrimaryHandTrigger),
            Tuple.Create(rightGrab, right, OVRInput.Button.PrimaryHandTrigger),
            Tuple.Create(buttonOne, right, OVRInput.Button.One),
            Tuple.Create(buttonOne, left, OVRInput.Button.One),
            Tuple.Create(buttonOne, touch, OVRInput.Button.One)
        };

        for (int i = 0; i < toCheck.Length; i++) {
            (bool pressed, OVRInput.OVRControllerBase ctrl, OVRInput.Button button) = toCheck[i];
            if (pressed) {
                ctrl.currentState.Buttons = ctrl.currentState.Buttons | (uint) ctrl.ResolveToRawMask(button);
            }
        }

        left.currentState.LHandTrigger = leftGrab ? 1 : 0;
        right.currentState.RHandTrigger = rightGrab ? 1 : 0;
    }
}