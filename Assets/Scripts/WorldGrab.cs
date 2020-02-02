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
            Vector3 offsetRot = transform.eulerAngles;

            transform.position = Vector3.zero;
            transform.localScale = Vector3.one;
            transform.eulerAngles = Vector3.zero;

            system.position = systemPos;
            system.localScale = new Vector3(newScale, newScale, newScale);
            system.eulerAngles += offsetRot;
        }

        bool left = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool right = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        if (left && !right) {
            transform.position = leftPos - leftGrabPos;
        } else if (!left && right) {
            transform.position = rightPos - rightGrabPos;
        } else if (left && right) {
            // Set scale
            float grabDist = Vector3.Distance(leftGrabPos, rightGrabPos);
            float dist = Vector3.Distance(leftPos, rightPos);
            float scale = dist / grabDist;
            transform.localScale = new Vector3(scale, scale, scale);

            // Set rotation
            float grabAngle = GetAngle(leftGrabPos, rightGrabPos);
            float angle = GetAngle(leftPos, rightPos);
            float diff = (grabAngle - angle);
            transform.eulerAngles = new Vector3(0, diff, 0);

            // Set position
            Vector3 handGrabMidpoint = (leftGrabPos + rightGrabPos) / 2;
            Vector3 scaled = handGrabMidpoint * scale;
            Vector3 pivot = new Vector3(0, scaled.y, 0);
            Vector3 rotated = Quaternion.Euler(transform.eulerAngles) * (scaled - pivot) + pivot;
            transform.position = handMidpoint - rotated;
        }
    }

    private float GetAngle(Vector3 p1, Vector3 p2) {
        return Mathf.Atan2(p2.z - p1.z, p2.x - p1.x) * Mathf.Rad2Deg;
    }
}