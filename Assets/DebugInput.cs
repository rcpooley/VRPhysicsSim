using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DebugInput : MonoBehaviour {

    public bool _enabled;

    public bool leftGrab;
    private bool lastLeftGrab;

    public bool rightGrab;
    private bool lastRightGrab;

    private OVRInput.OVRControllerBase left;

    private OVRInput.OVRControllerBase right;

    void Start() {
        Type inType = typeof(OVRInput);
        FieldInfo ctrlField = inType.GetField("controllers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        List<OVRInput.OVRControllerBase> controllers = (List<OVRInput.OVRControllerBase>) ctrlField.GetValue(null);
        controllers.ForEach(c => {
            if (c.controllerType == OVRInput.Controller.LTouch) {
                left = c;
            } else if (c.controllerType == OVRInput.Controller.RTouch) {
                right = c;
            }
        });
    }

    void Update() {
        OVRInput.debugMode = _enabled;
        lastLeftGrab = UpdateController(left, lastLeftGrab, leftGrab, false);
        lastRightGrab = UpdateController(right, lastRightGrab, rightGrab, true);

        float flex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        DebugPanel.U("Right flex", flex);
    }

    private bool UpdateController(OVRInput.OVRControllerBase ctrl, bool last, bool now, bool right) {
        if (last != now) {
            if (now) {
                ctrl.currentState.Buttons = (uint) ctrl.ResolveToRawMask(OVRInput.Button.PrimaryHandTrigger);
                ctrl.previousState.Buttons = 0;
                if (right)
                    ctrl.currentState.RHandTrigger = 1;
                else
                    ctrl.currentState.LHandTrigger = 1;
            } else {
                ctrl.currentState.Buttons = 0;
                if (right)
                    ctrl.currentState.RHandTrigger = 0;
                else
                    ctrl.currentState.LHandTrigger = 0;
            }
        } else {
            ctrl.previousState = ctrl.currentState;
        }
        return now;
    }
}