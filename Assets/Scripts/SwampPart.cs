using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampPart : SwampTouchable {

    private MeshRenderer m_renderer;

    private Material startMaterial;

    void Start() {
        m_renderer = GetComponent<MeshRenderer>();
        startMaterial = m_renderer.material;
    }

    public SwampObject swampObject {
        get { return transform.parent.GetComponent<SwampObject>(); }
    }

    public override void OnTouch() { }

    public override void OffTouch() { }

    public void SetEnabled(bool enabled) {
        if (m_renderer)
            m_renderer.material = enabled ? startMaterial : Ref.disabledObjectMaterial;
    }
}