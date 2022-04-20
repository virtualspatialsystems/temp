using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SetMaterialProperty : MonoBehaviour
{

    public Material material;


    // Update is called once per frame
    void Update()
    {
        material.SetVector("_Position", transform.position);
        material.SetVector("_BoxSize", transform.localScale);
    }
}
