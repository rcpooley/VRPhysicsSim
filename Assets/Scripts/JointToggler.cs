using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointToggler : MonoBehaviour {

    public GameObject jointToggleCollider;

    private GameObject jointCollider;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void OnGridSnap(GridSnapper snapper) {
        if (jointCollider) {
            Destroy(jointCollider);
        }

        jointCollider = Instantiate(jointToggleCollider);
        Transform t = jointCollider.transform;
        t.parent = transform.parent; // set part's parent object as parent
        t.localPosition = transform.localPosition;
        t.localScale = transform.localScale * 1.5f;
    }
}