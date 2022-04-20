using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyRotation : MonoBehaviour
{
    public GameObject toFollowObject;
    public float height = 0.5f;
    public float easeIn = .5f;
    public bool verticalRotationOn = false;
    private Vector3 currentPos;
    private Vector3 velocity = Vector3.zero;

    private void UpdatePosition()
    {

        // Define a target position above and behind the target transform
        transform.position = new Vector3(toFollowObject.transform.position.x, toFollowObject.transform.position.y + height, toFollowObject.transform.position.z);
       
        Vector3 targetRotation = new Vector3(toFollowObject.transform.forward.x, toFollowObject.transform.forward.y, toFollowObject.transform.forward.z);
        if (!verticalRotationOn)
        {
            targetRotation = new Vector3(toFollowObject.transform.forward.x, 0, toFollowObject.transform.forward.z);
        }

        // Smoothly move the camera towards that target position
        transform.forward = Vector3.SmoothDamp(transform.forward, targetRotation, ref velocity, easeIn);
    }


    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
}
