using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEditor : MonoBehaviour
{
    bool editing;

    public GameObject floor;
    Material normalFloor;
    public Material editingFloor;

    public GameObject resizeCube;
    GameObject cubeInstance;
    public Transform centerEyeAnchor;


    void Start()
    {
        editing=false;
        normalFloor=floor.GetComponent<MeshRenderer>().material;

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One)){
            toggleEditing();
        }

        if(editing){
            if (OVRInput.GetDown(OVRInput.Button.Two)){
                spawnCube();
            }
        }
    }

    void toggleEditing(){
        if(!editing){
            editing = true;
            floor.GetComponent<MeshRenderer>().material=editingFloor;
        }else{
            editing = false;
            floor.GetComponent<MeshRenderer>().material=normalFloor;
        }
    }

    void spawnCube(){
        Vector3 spawnCubePosition = centerEyeAnchor.position + centerEyeAnchor.forward*0.5f;
        if(!cubeInstance){
            cubeInstance = Instantiate(resizeCube,spawnCubePosition, Quaternion.Euler(0,0,0));
        }else{
            cubeInstance.transform.position=spawnCubePosition;
            cubeInstance.transform.rotation=Quaternion.Euler(0,0,0);
        }
    }
}
