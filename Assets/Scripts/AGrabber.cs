using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGrabber : MonoBehaviour {

    public OVRInput.Controller controller;

    private List<AGrabbable> touching;

    private AGrabbable grabbedObject;

    private Vector3 handGrabPos;
    private Vector3 handGrabRot;
    private Vector3 objGrabPos;
    private Vector3 objGrabRot;

    // Start is called before the first frame update
    void Start() {
        touching = new List<AGrabbable>();
    }

    // Update is called once per frame
    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, controller)) {
            grabbedObject = getClosestObject();
            handGrabPos = transform.position;
            handGrabRot = transform.eulerAngles;
            objGrabPos = grabbedObject.transform.position;
            objGrabRot = grabbedObject.transform.eulerAngles;
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, controller)) {
            grabbedObject = null;
        }

        if (grabbedObject) {
            Vector3 newPos = objGrabPos + (transform.position - handGrabPos);
            Vector3 rot = objGrabRot + transform.eulerAngles - handGrabRot;
            Vector3 rotated = Quaternion.Euler(rot) * (newPos - transform.position) + transform.position;
            grabbedObject.transform.position = rotated;
            grabbedObject.transform.eulerAngles = rot;
        }
    }

    void OnTriggerEnter(Collider other) {
        AGrabbable ag = other.GetComponent<AGrabbable>();
        if (!ag) {
            return;
        }
        if (!touching.Contains(ag)) {
            touching.Add(ag);
        }
    }

    void OnTriggerExit(Collider other) {
        AGrabbable ag = other.GetComponent<AGrabbable>();
        if (!ag) {
            return;
        }
        touching.Remove(ag);
    }

    private AGrabbable getClosestObject() {
        AGrabbable closest = null;
        float closestDist = 0;
        touching.ForEach(ag => {
            float dist = Vector3.Distance(transform.position, ag.transform.position);
            if (closest == null || dist < closestDist) {
                closest = ag;
                closestDist = dist;
            }
        });
        return closest;
    }

    public bool isGrabbing() {
        return touching.Count > 0 || grabbedObject != null;
    }
}