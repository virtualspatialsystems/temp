using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class VignetteController : MonoBehaviour
{
    public Material CameraMaterial;
    public Material SkyMaterial;
    public Color SkyColor;
    public float vignetteStrength = -2;
    private float closed = 2;
    private float open = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //CameraMaterial.SetFloat("_AddVignette", vignetteStrength);
        SkyMaterial.SetColor("_Tint", SkyColor);

    }

    public void OpenVignette(float lerpValue) {
        vignetteStrength = Mathf.Lerp(closed, open, lerpValue);
    }

    public void CloseVignette(float lerpValue)
    {
        vignetteStrength = Mathf.Lerp(open, closed, lerpValue);
    }
}
