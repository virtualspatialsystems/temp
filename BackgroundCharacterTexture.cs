using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using System;

[ExecuteAlways]
public class BackgroundCharacterTexture : MonoBehaviour
{
    MaterialPropertyBlock _block;
    public Texture2D img;
    private float _alpha = 1;
    public float duration = 1;
    public bool fadeIn = false;
    public bool fadeOut = false;
    public Material _glowMat;

    [Range(0.0f, 1.0f)]
    public float colorStrength = 1;

    [Tooltip("Soll das Objekt Schatten werfen?(default=false)")]
    public bool castRealtimeShadows = false;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Awake()
    {
        SetMaterialBlock();
        CustomGlowObj _glowObj = GetComponent<CustomGlowObj>();
        if(_glowObj == null)
        {
            _glowObj = gameObject.AddComponent<CustomGlowObj>();
        }

        Material glow_mat = new Material(_glowMat.shader);
        glow_mat.SetTexture("_MainTex", img);

        _glowObj.glowMaterial = glow_mat;
        

        _renderer = GetComponent<Renderer>();

        if(!castRealtimeShadows){
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }else{
            _renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
    }
    private void Update()
    {

    }

    public void FadeOut()
    {

            TweenFactory.Tween("fadeTexture-" + System.Guid.NewGuid(),1, 0, duration, TweenScaleFunctions.Linear, (v1) =>
            {

                try
                {
                    _alpha = v1.CurrentValue;
                    SetMaterialBlock();
                }
                catch (Exception e)
                {
                }


            }, (v2) => { 

                foreach(Transform t in GetComponentInChildren<Transform>()) {
                    t.gameObject.SetActive(false);
                }
            });
    }

    public void FadeIn() {
        foreach (Transform t in GetComponentInChildren<Transform>())
        {
            t.gameObject.SetActive(true);
        }

        TweenFactory.Tween("fadeTexture-" + System.Guid.NewGuid(), 0, 1, duration, TweenScaleFunctions.Linear, (v1) =>
        {


            try
            {
                _alpha = v1.CurrentValue;
                SetMaterialBlock();

            }
            catch (Exception e)
            {
            }


        }, (v2) =>
        {

        });
    }

    void OnEnable()
    {
            //FadeIn(); 
    }

    private void OnTriggerEnter(Collider c)
    {

        /*if (c.gameObject.tag == "MainCamera" && !fadeOut)
        {
            fadeOut = true;
            FadeOut();
        }*/
    }

    private void OnTriggerExit(Collider c)
    {
        /*if (c.gameObject.tag == "MainCamera" && !fadeIn)
        {

            fadeIn = true;
         */
    }

    void SetMaterialBlock() {
        _block = new MaterialPropertyBlock();
        GetComponent<Renderer>().GetPropertyBlock(_block);
        _block.SetTexture("_MainTex", img);
        _block.SetFloat("_alpha", _alpha);
        _block.SetFloat("_ColorStrength", colorStrength);

        GetComponent<Renderer>().SetPropertyBlock(_block);
    }

}
