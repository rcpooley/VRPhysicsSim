using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrab : MonoBehaviour {

    private Transform system;

    private Vector3 leftGrabPos;

    private Vector3 rightGrabPos;

    // Start is called before the first frame update
    void Start() {
        system = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update() {
        Vector3 leftPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 rightPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 handMidpoint = (leftPos + rightPos) / 2;

        bool leftDown = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool leftUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightDown = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        bool rightUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (leftDown || rightDown || leftUp || rightUp) {
            leftGrabPos = leftPos;
            rightGrabPos = rightPos;

            Vector3 systemPos = system.position;
            float newScale = system.localScale.x * transform.localScale.x;

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;

            system.position = systemPos;
            system.localScale = new Vector3(newScale, newScale, newScale);
        }

        bool left = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool right = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (left && !right) {
            transform.position = leftPos - leftGrabPos;
        } else if (!left && right) {
            transform.position = rightPos - rightGrabPos;
        } else if (left && right) {
            float grabDist = Vector3.Distance(leftGrabPos, rightGrabPos);
            float dist = Vector3.Distance(leftPos, rightPos);
            float scale = dist / grabDist;
            transform.localScale = new Vector3(scale, scale, scale);

            Vector3 handGrabMidpoint = (leftGrabPos + rightGrabPos) / 2;
            transform.position = handMidpoint - handGrabMidpoint * scale;
        }
    }
}