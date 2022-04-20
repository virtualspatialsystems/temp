using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerForScreengrab : MonoBehaviour
{
    public Camera TheCamera;
    public KeyCode ResetPitch = KeyCode.Keypad9;
    public KeyCode AnglePlus = KeyCode.KeypadPlus;
    public KeyCode AngleMinus = KeyCode.KeypadMinus;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ResetPitch))
        {
            TheCamera.transform.eulerAngles = new Vector3(0, TheCamera.transform.eulerAngles.y, TheCamera.transform.eulerAngles.z);
        }

        if (Input.GetKeyDown(AnglePlus)) TheCamera.fieldOfView = TheCamera.fieldOfView + 5;
        if (Input.GetKeyDown(AngleMinus)) TheCamera.fieldOfView = TheCamera.fieldOfView - 5;
    }
}
