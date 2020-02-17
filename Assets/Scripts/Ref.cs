using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref : MonoBehaviour {

    private static Ref instance;
    private static Ref getInstance() {
        if (!instance) {
            instance = FindObjectOfType<Ref>();
        }
        return instance;
    }

    public static List<SwampObject> swampObjects {
        get {
            List<SwampObject> l = new List<SwampObject>();
            Util.Children(objects).ForEach(s => l.Add(s.GetComponent<SwampObject>()));
            return l;
        }
    }

    public GameObject _floor;
    public static GameObject floor {
        get { return getInstance()._floor; }
    }

    public Material _editingFloorMaterial;
    public static Material editingFloorMaterial {
        get { return getInstance()._editingFloorMaterial; }
    }

    public Transform _centerEyeAnchor;
    public static Transform centerEyeAnchor {
        get { return getInstance()._centerEyeAnchor; }
    }

    public GameObject _part;
    public static GameObject part {
        get { return getInstance()._part; }
    }

    public AGrabber _leftHand;
    public static AGrabber leftHand {
        get { return getInstance()._leftHand; }
    }

    public AGrabber _rightHand;
    public static AGrabber rightHand {
        get { return getInstance()._rightHand; }
    }

    public Transform _objects;
    public static Transform objects {
        get { return getInstance()._objects; }
    }

    public Material _disabledObjectMaterial;
    public static Material disabledObjectMaterial {
        get { return getInstance()._disabledObjectMaterial; }
    }
}