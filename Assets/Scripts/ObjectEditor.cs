using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour {

    public static ObjectEditor instance {
        get { return FindObjectOfType<ObjectEditor>(); }
    }

    private MeshRenderer floorMeshRenderer;

    private Material normalFloor;

    private SwampObject m_activeObject;

    private SwampObject m_touchingObject;

    public SwampObject activeObject {
        get { return m_activeObject; }
    }

    public void SetTouchingObject(SwampObject obj) {
        m_touchingObject = obj;
    }

    void Start() {
        floorMeshRenderer = Ref.floor.GetComponent<MeshRenderer>();
        normalFloor = floorMeshRenderer.material;
    }

    void Awake() {
        setSystemMode(true);
    }

    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.One)) {
            if (m_activeObject) {
                // Toggle editing off if we're currently editing
                setSystemMode(true);

                // Make each part not grabbable
                m_activeObject.parts.ForEach(p => {
                    Destroy(p.GetComponent<AGrabbable>());
                });

                m_activeObject = null;
            } else if (m_touchingObject) {
                // Otherwise start editing the object we're currently touching
                m_activeObject = m_touchingObject;
                m_touchingObject = null;

                setSystemMode(false);

                // Make each part grabbable
                m_activeObject.parts.ForEach(p => {
                    p.gameObject.AddComponent<AGrabbable>();
                });
            }
        }

        if (m_activeObject && OVRInput.GetDown(OVRInput.Button.Two)) {
            spawnCube();
        }
    }

    void spawnCube() {
        // Vector3 spawnCubePosition = Ref.centerEyeAnchor.position + Ref.centerEyeAnchor.forward * 0.5f;
        // Transform t = Instantiate(Ref.part, spawnCubePosition, Quaternion.identity).transform;
    }

    private void setSystemMode(bool isSystem) {
        // If setting to system mode, make each object grabbable
        Ref.swampObjects.ForEach(obj => {
            obj.parts.ForEach(p => p.SetEnabled(obj == m_activeObject));
            if (isSystem) {
                obj.gameObject.AddComponent<AGrabbable>();
            } else {
                Destroy(obj.GetComponent<AGrabbable>());
            }
        });

        floorMeshRenderer.material = isSystem ? normalFloor : Ref.editingFloorMaterial;
    }
}