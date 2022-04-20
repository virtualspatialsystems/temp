using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float rootXRotation;
    public float rootZRotation;
    public float rootYPosition;

    private float rootYRotation;
    public float yRotationStrength = 5;
    public float yRotationspeed = 5;

    
    public float yPositionStrength = 5;
    public float yPositionspeed = 5;

    private float rootXPosition;
    public float xPositionStrength = 5;
    public float xPositionspeed = 5;

    public bool isRandom = true;
    // Start is called before the first frame update
    void Start()
    {
        //rootYRotation = transform.localRotation.y;
        //rootXRotation = transform.localRotation.x;
        //rootZRotation = transform.localRotation.z;

        rootYPosition = transform.position.y;
        rootXPosition = transform.position.x;



        if(isRandom){
            yRotationStrength = Random.Range(0,yRotationStrength);
            yRotationspeed  = Random.Range(0,yRotationspeed);

            yPositionStrength = Random.Range(0,yPositionStrength);
            yPositionspeed  = Random.Range(0,yPositionspeed);

            xPositionStrength = Random.Range(0,xPositionStrength);
            xPositionspeed  = Random.Range(0,xPositionspeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localRotation = Quaternion.Euler(rootXRotation, rootYRotation + Mathf.Sin(Time.time*yRotationspeed)*yRotationStrength, rootZRotation);
        transform.position = new Vector3(rootXPosition + Mathf.Sin(Time.time*xPositionspeed)*xPositionStrength, rootYPosition + Mathf.Sin(Time.time*yPositionspeed)*yPositionStrength, transform.position.z);
    }
}
