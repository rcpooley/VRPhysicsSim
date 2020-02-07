using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

    public static List<Transform> GetSiblings(Transform me) {
        List<Transform> sibs = new List<Transform>();
        for (int i = me.parent.childCount - 1; i >= 0; i--) {
            Transform sib = me.parent.GetChild(i);
            if (sib != me) {
                sibs.Add(sib);
            }
        }
        return sibs;
    }

}