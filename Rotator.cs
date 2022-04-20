using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rotator : MonoBehaviour
{
    public GameObject Origin;
    public GameObject CopyObject;
    public int Count = 5;
    public int Angle = 360;


    void Init()
    {
        Origin = Origin ? Origin : gameObject;

        for (var i = 0; i < Count; i++)
        {
            GameObject _o = Instantiate(CopyObject, Origin.transform);

            _o.transform.rotation = Quaternion.AngleAxis(360 / Count * i, Vector3.up);
        }
    }

    public void Create()
    {
        
        foreach (Transform t in transform)
        {
            DestroyImmediate(t.gameObject);
        }

        Init();
    }

}
