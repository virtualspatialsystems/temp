using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecenterer : MonoBehaviour
{
    [Tooltip("Parent Object der Camera")]
    public GameObject cameraHolder;
    [Tooltip("VR Camera")]
    public GameObject camera;
    [Tooltip("Target Object der Camera")]
    public GameObject target;

    private void Start()
    {
        GameObject _rotIndicator = new GameObject();
        _rotIndicator.transform.parent = cameraHolder.transform;
        _rotIndicator.transform.localPosition = Vector3.zero;
        _rotIndicator.transform.LookAt(target.transform);

        Quaternion _rot = _rotIndicator.transform.rotation * Quaternion.Inverse(camera.transform.rotation);

        cameraHolder.transform.rotation *= _rot;

        Vector3 _eulerAngles = cameraHolder.transform.rotation.eulerAngles;
        _eulerAngles.x = 0;
        _eulerAngles.z = 0;

        cameraHolder.transform.eulerAngles = _eulerAngles;
    }
}
