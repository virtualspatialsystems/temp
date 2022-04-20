using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class DefaultGlowObj : MonoBehaviour
{
    public Material renderMat;
    public Texture img;
    [HideInInspector]
    public Material _glow_mat;

    // Start is called before the first frame update
    private void Awake()
    {
        CustomGlowObj _glowObj = GetComponent<CustomGlowObj>();
        if (_glowObj == null)
        {
            _glowObj = gameObject.AddComponent<CustomGlowObj>();
        }

        Material glow_mat = new Material(renderMat);
        glow_mat.SetTexture("_MainTex", img);

        _glowObj.glowMaterial = glow_mat;
    }
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
