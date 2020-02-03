using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapper : MonoBehaviour {

    private static float unitSize = 0.1f;

    private Transform system;

    public int x, y, z;

    public int xs = 1, ys = 1, zs = 1;

    public bool noScale;

    public bool runtimeCalculate;

    private JointToggler jointToggler;

    // Start is called before the first frame update
    void Start() {
        WorldGrab wg = Object.FindObjectOfType<WorldGrab>();
        system = wg.getSystem();

        jointToggler = GetComponent<JointToggler>();

        if (runtimeCalculate) {
            GridSnap(x, y, z);
        } else {
            GridSnap();
        }
    }

    // Update is called once per frame
    void Update() { }

    public void GridSnap() {
        // Snap rotation
        Vector3 rot = transform.localEulerAngles / 90;
        Vector3 snapRot = new Vector3(
            Mathf.Round(rot.x),
            Mathf.Round(rot.y),
            Mathf.Round(rot.z)
        ) * 90;
        Debug.Log("Rot: " + transform.localEulerAngles);
        Debug.Log("SnapRot: " + snapRot);
        Debug.Log("RotOffset: " + (snapRot - transform.localEulerAngles));
        Bounds bounds = transform.GetComponent<BoxCollider>().bounds;
        Vector3 min = bounds.min - bounds.center;
        Vector3 max = bounds.max - bounds.center;
        Debug.Log("Min: " + min);
        Debug.Log("Max: " + max);
        Quaternion rot1 = Quaternion.Euler(transform.localEulerAngles - snapRot);
        Quaternion rot2 = Quaternion.Euler(snapRot - transform.localEulerAngles);
        float scaleOff = system.localScale.x * unitSize;
        Vector3 diff1 = (rot1 * max - rot1 * min) / scaleOff;
        Vector3 diff2 = (rot2 * max - rot2 * min) / scaleOff;
        Debug.Log("Diff1: " + diff1);
        Debug.Log("Diff2: " + diff2);

        Vector3 scale = new Vector3(xs * unitSize, ys * unitSize, zs * unitSize);
        Vector3 corner = transform.localPosition - scale / 2;
        Vector3 rawPos = corner / unitSize;
        int x = (int) Mathf.Round(rawPos.x);
        int y = (int) Mathf.Round(rawPos.y);
        int z = (int) Mathf.Round(rawPos.z);
        GridSnap(x, y, z);
    }

    public void GridSnap(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;

        Vector3 scale = new Vector3(xs * unitSize, ys * unitSize, zs * unitSize);
        Vector3 newPos = new Vector3(x, y, z) * unitSize;
        if (!noScale) {
            transform.localScale = scale;
            newPos += scale / 2;
        }
        transform.localPosition = newPos;
        transform.eulerAngles = Vector3.zero;

        if (jointToggler) {
            jointToggler.OnGridSnap(this);
        }
    }
}