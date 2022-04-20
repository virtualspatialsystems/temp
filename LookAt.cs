using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;
    public float angle;
    public float distance;
    public float floorOffset;
    public Vector3 offsetRotation;

    public void Place()
    {
        Vector3 cameraForward = Camera.main.transform.forward * distance;
        cameraForward.y = target.transform.position.y;

        Vector3 targetFloorPos = target.transform.position + new Vector3(0, floorOffset, 0);

        transform.position = new Vector3(targetFloorPos.x + cameraForward.x, targetFloorPos.y, targetFloorPos.z + cameraForward.z);

        Vector3 diffPos = targetFloorPos - transform.position;

        transform.rotation = Quaternion.LookRotation(diffPos , Vector3.up) * Quaternion.Euler(offsetRotation);
    }

    public void Update()
    {
       // Place();
    }

    Vector3 PointOnCircle(Vector3 offset, float angle, float radius)
    {
        float x = offset.x + radius * Mathf.Sin(angle);
        float z = offset.z + radius * Mathf.Cos(angle);

        return new Vector3(x, offset.y + floorOffset , z);
    }
}
