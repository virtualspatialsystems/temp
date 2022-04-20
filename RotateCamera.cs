using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rotateCamera() {
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y = -180;
        transform.rotation = Quaternion.EulerAngles(rot);
    }
}
