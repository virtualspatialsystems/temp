using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPosition : MonoBehaviour
{
    public List<Vector3> positions;
    public List<string> names;
    public GameObject CameraObj;
    // Start is called before the first frame update
    void Start()
    {
        CameraObj.transform.position = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            CameraObj.transform.position = positions[0];
        }
        else if (Input.GetKeyDown("2")) {
            CameraObj.transform.position = positions[1];
        }
        else if (Input.GetKeyDown("3"))
        {
            CameraObj.transform.position = positions[2];
        }
        else if (Input.GetKeyDown("4"))
        {
            CameraObj.transform.position = positions[3];
        }
        else if (Input.GetKeyDown("5"))
        {
            CameraObj.transform.position = positions[4];
        }
        else if (Input.GetKeyDown("6"))
        {
            CameraObj.transform.position = positions[5];
        }
        else if (Input.GetKeyDown("7"))
        {
            CameraObj.transform.position = positions[6];
        }
        else if (Input.GetKeyDown("8"))
        {
            CameraObj.transform.position = positions[7];
        }
        else if (Input.GetKeyDown("9"))
        {
            CameraObj.transform.position = positions[8];
        }
        else if (Input.GetKeyDown("q"))
        {
            CameraObj.transform.position = positions[9];
        }
        else if (Input.GetKeyDown("w"))
        {
            CameraObj.transform.position = positions[10];
        }
        else if (Input.GetKeyDown("e"))
        {
            CameraObj.transform.position = positions[11];
        }
        else if (Input.GetKeyDown("r"))
        {
            CameraObj.transform.position = positions[12];
        }
        else if (Input.GetKeyDown("t"))
        {
            CameraObj.transform.position = positions[13];
        }
        else if (Input.GetKeyDown("z"))
        {
            CameraObj.transform.position = positions[14];
        }

    }
}
