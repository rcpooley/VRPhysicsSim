using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwampObject : Touchable {

    public List<SwampPart> parts {
        get {
            List<SwampPart> p = new List<SwampPart>();
            Util.Children(transform).ForEach(c => p.Add(c.GetComponent<SwampPart>()));
            return p;
        }
    }

    public override void OnTouch() {
        ObjectEditor.instance.SetTouchingObject(this);
    }

    public override void OffTouch() {
        // TODO test going from one object right to another
        ObjectEditor.instance.SetTouchingObject(null);
    }
}