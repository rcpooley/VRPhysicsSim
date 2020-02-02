using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrab : MonoBehaviour {

    private Transform systemOffset;

    private Vector3 systemGrabPos;
    private float systemGrabScale;

    private Vector3 leftGrabPos;

    private Vector3 rightGrabPos;

    // Start is called before the first frame update
    void Start() {
        systemOffset = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update() {
        Vector3 leftPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
        Vector3 rightPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        Vector3 systemPos = transform.position;
        float systemScale = transform.localScale.x;

        bool leftDown = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool leftUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightDown = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        bool rightUp = OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (leftDown || rightDown || leftUp || rightUp) {
            leftGrabPos = leftPos;
            rightGrabPos = rightPos;
            systemGrabPos = systemPos;
            systemGrabScale = systemScale;
        }

        bool left = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool right = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (left && !right) {
            Vector3 offset = leftPos - leftGrabPos;
            transform.position = systemGrabPos + offset;
        } else if (!left && right) {
            Vector3 offset = rightPos - rightGrabPos;
            transform.position = systemGrabPos + offset;
        } else if (left && right) {
            float grabDist = Vector3.Distance(leftGrabPos, rightGrabPos);
            float dist = Vector3.Distance(leftPos, rightPos);
            float scale = dist / grabDist;
            float newScale = systemGrabScale * scale;
            transform.localScale = new Vector3(newScale, newScale, newScale);

            Vector3 handMidpoint = (leftPos + rightPos) / 2;
            Vector3 offset = systemGrabPos - handMidpoint;
            transform.position = handMidpoint + offset * newScale;
        }
    }
}