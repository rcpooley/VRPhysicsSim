using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour {

    private MeshRenderer floorMeshRenderer;

    private Material normalFloor;

    private bool editing;

    void Start() {
        floorMeshRenderer = Ref.floor.GetComponent<MeshRenderer>();
        normalFloor = floorMeshRenderer.material;
    }

    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            toggleEditing();
        }

        if (editing) {
            if (OVRInput.GetDown(OVRInput.Button.Two)) {
                spawnCube();
            }
        }
    }

    void toggleEditing() {
        if (!editing) {
            editing = true;
            floorMeshRenderer.material = Ref.editingFloorMaterial;
        } else {
            editing = false;
            floorMeshRenderer.material = normalFloor;
        }
    }

    void spawnCube() {
        Vector3 spawnCubePosition = Ref.centerEyeAnchor.position + Ref.centerEyeAnchor.forward * 0.5f;
        Transform t = Instantiate(Ref.part, spawnCubePosition, Quaternion.identity).transform;
    }
}