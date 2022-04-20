using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public bool rotateY = false;
    public bool rotateX = false;
    public bool rotateZ = false;

    private Vector3 startAngles;
    // Start is called before the first frame update
    void Start()
    {
        startAngles = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(Camera.main.transform);
        Vector3 eulerAngles = transform.eulerAngles;
        if (!rotateY) {
            eulerAngles.y = startAngles.y;
        }

        if (!rotateX)
        {
            eulerAngles.x = startAngles.x;
        }

        if (!rotateZ)
        {
            eulerAngles.z = startAngles.z;
        }
        transform.eulerAngles = eulerAngles;

    }
}
