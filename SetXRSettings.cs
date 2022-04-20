using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SetXRSettings : MonoBehaviour
{
    public float TextureResolutionScale = 2.0f;

    void Start()
    {
        XRSettings.eyeTextureResolutionScale = TextureResolutionScale;
    }

}
