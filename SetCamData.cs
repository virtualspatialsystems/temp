using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamData : MonoBehaviour
{
    public Material _material;
   
    void Update()
    {
        _material.SetVector("_CameraForward", transform.forward);
    }
}
