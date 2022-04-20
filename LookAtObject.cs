using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public static LookAtObject instance;
    public Transform target;


    public GameObject LookObject;
    public GameObject WalkObject;

    public bool TargetTypeLook = true;

    void Start()
    {
        instance = this;
    }


    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);

   

        if (TargetTypeLook)
        {
           LookObject.SetActive(true);
           WalkObject.SetActive(false);
        }


        if (!TargetTypeLook)
        {
            LookObject.SetActive(false);
            WalkObject.SetActive(true);
        }
    }


}