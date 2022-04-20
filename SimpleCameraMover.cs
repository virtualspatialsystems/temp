using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMover : MonoBehaviour
{

    public KeyCode KeyForward = KeyCode.W;
    public KeyCode KeyBack = KeyCode.S;
    public KeyCode KeyLeft = KeyCode.A;
    public KeyCode KeyRight = KeyCode.D;
    public KeyCode KeyUp = KeyCode.PageUp;
    public KeyCode KeyDown = KeyCode.PageDown;
    public float MovementSpeed = 2f;

    public KeyCode ActiveRotation = KeyCode.Mouse1;

    Vector2 rotation = new Vector2(0, 0);
    public float RotationSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyForward)) transform.Translate(Vector3.forward * Time.deltaTime * MovementSpeed);
        if (Input.GetKey(KeyBack)) transform.Translate(Vector3.back * Time.deltaTime * MovementSpeed);
        if (Input.GetKey(KeyLeft)) transform.Translate(Vector3.left * Time.deltaTime * MovementSpeed);
        if (Input.GetKey(KeyRight)) transform.Translate(Vector3.right * Time.deltaTime * MovementSpeed);
        if (Input.GetKey(KeyUp)) transform.Translate(Vector3.up * Time.deltaTime * MovementSpeed);
        if (Input.GetKey(KeyDown)) transform.Translate(Vector3.down * Time.deltaTime * MovementSpeed);


        if (Input.GetKey(ActiveRotation))
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            transform.eulerAngles = (Vector2)rotation * RotationSpeed;
        }
    }
}
