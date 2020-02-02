using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnapper : MonoBehaviour {

    private static float unitSize = 0.1f;

    public int x, y, z;

    public int xs = 1, ys = 1, zs = 1;

    public bool debugSnap;

    // Start is called before the first frame update
    void Start() {
        GridSnap();
    }

    // Update is called once per frame
    void Update() {
        if (debugSnap) {
            debugSnap = false;
            GridSnap();
        }
    }

    void GridSnap() {
        Vector3 scale = new Vector3(xs * unitSize, ys * unitSize, zs * unitSize);
        Vector3 corner = transform.position - scale / 2;
        Vector3 rawPos = corner / unitSize;
        x = (int) Mathf.Round(rawPos.x);
        y = (int) Mathf.Round(rawPos.y);
        z = (int) Mathf.Round(rawPos.z);
        transform.localPosition = new Vector3(
            x * unitSize + scale.x / 2,
            y * unitSize + scale.y / 2,
            z * unitSize + scale.z / 2
        );
        transform.localScale = scale;
    }
}