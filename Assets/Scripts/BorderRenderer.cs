using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderRenderer : MonoBehaviour {

    private static float thin = 0.008f;

    private static int X = 0;
    private static int Y = 1;
    private static int Z = 2;
    private static int UP = 3;
    private static int DOWN = 4;
    private static int LEFT = 5;
    private static int RIGHT = 6;
    private static int FRONT = 7;
    private static int BACK = 8;

    public GameObject border;

    private Transform borders;

    private Dictionary<int, List<Transform>> bmap;

    // Start is called before the first frame update
    void Start() {
        borders = Instantiate(new GameObject()).transform;
        borders.parent = transform;
        borders.localPosition = Vector3.zero;
        borders.localEulerAngles = Vector3.zero;

        bmap = new Dictionary<int, List<Transform>>();
        for (int i = 0; i < 9; i++) {
            bmap[i] = new List<Transform>();
        }

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 2; j++) {
                CreateBorder(i, j == 0, true);
                CreateBorder(i, j == 0, false);
            }
        }

        Resize();
    }

    // Update is called once per frame
    void Update() { }

    /**
        x: up/down,    front/back
        y: left/right, front/back
        z: up/down,    left/right
    */
    private void CreateBorder(int xyz, bool a, bool b) {
        GameObject obj = Instantiate(border);
        Transform t = obj.transform;
        t.parent = borders;
        t.localEulerAngles = Vector3.zero;

        if (xyz == 0) {
            bmap[X].Add(t);
            bmap[a ? UP : DOWN].Add(t);
            bmap[b ? FRONT : BACK].Add(t);
        } else if (xyz == 1) {
            bmap[Y].Add(t);
            bmap[a ? LEFT : RIGHT].Add(t);
            bmap[b ? FRONT : BACK].Add(t);
        } else {
            bmap[Z].Add(t);
            bmap[a ? UP : DOWN].Add(t);
            bmap[b ? LEFT : RIGHT].Add(t);
        }
    }

    private void Resize() {
        Vector3 s = transform.localScale;

        borders.localScale = new Vector3(
            1 / s.x,
            1 / s.y,
            1 / s.z
        );

        for (int i = 0; i < borders.childCount; i++) {
            Transform t = borders.GetChild(i);
            float x = 0, y = 0, z = 0;
            float xs = thin, ys = thin, zs = thin;

            // X, Y, Z
            if (bmap[X].Contains(t)) {
                xs += s.x;
            } else if (bmap[Y].Contains(t)) {
                ys += s.y;
            } else {
                zs += s.z;
            }

            // UP, DOWN
            if (bmap[UP].Contains(t)) {
                y = s.y / 2;
            } else if (bmap[DOWN].Contains(t)) {
                y = -s.y / 2;
            }

            // LEFT, RIGHT
            if (bmap[LEFT].Contains(t)) {
                x = s.x / 2;
            } else if (bmap[RIGHT].Contains(t)) {
                x = -s.x / 2;
            }

            // FRONT, BACK
            if (bmap[FRONT].Contains(t)) {
                z = s.z / 2;
            } else if (bmap[BACK].Contains(t)) {
                z = -s.z / 2;
            }

            t.localPosition = new Vector3(x, y, z);
            t.localScale = new Vector3(xs, ys, zs);
        }
    }
}