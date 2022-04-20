using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LazyFollow : MonoBehaviour
{
    public GameObject toFollowObject;
    public float speed = .5f;
    private Vector3 currentPos;
    private Vector3 velocity = Vector3.zero;

    private void UpdatePosition()
    {
        // Define a target position above and behind the target transform
        Vector3 targetPosition = toFollowObject.transform.position;

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity ,speed);
    }


    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
}
