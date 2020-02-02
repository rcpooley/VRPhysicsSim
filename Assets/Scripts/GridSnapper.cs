using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapper : MonoBehaviour {

    private static float unitSize = 0.1f;

    public int x, y, z;

    public int xs = 1, ys = 1, zs = 1;

    public bool noScale;

    public bool runtimeCalculate;

    private JointToggler jointToggler;

    // Start is called before the first frame update
    void Start() {
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
        Vector3 scale = new Vector3(xs * unitSize, ys * unitSize, zs * unitSize);
        Vector3 corner = transform.localPosition - scale / 2;
        Vector3 rawPos = corner / unitSize;
        int x = (int) Mathf.Round(rawPos.x);
        int y = (int) Mathf.Round(rawPos.y);
        int z = (int) Mathf.Round(rawPos.z);
        GridSnap(x, y, z);
    }

    public void GridSnap(int x, int y, int z) {
        Vector3 scale = new Vector3(xs * unitSize, ys * unitSize, zs * unitSize);
        this.x = x;
        this.y = y;
        this.z = z;
        Vector3 newPos = new Vector3(x, y, z) * unitSize;
        if (!noScale) {
            transform.localScale = scale;
            newPos += scale / 2;
        }
        transform.localPosition = newPos;

        if (jointToggler) {
            jointToggler.OnGridSnap(this);
        }
    }
}