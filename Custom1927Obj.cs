using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class Custom1927Obj : MonoBehaviour
{
    public Material renderMat;
    public Texture img;
    [HideInInspector]
    public Material finalRenderMat;
    public Material PostEffect;

    private Renderer _renderer;
    // Start is called before the first frame update
    private void Awake()
    {

        _renderer = GetComponent<Renderer>();
        PostEffect.SetFloat("_TitleLerpWhite", 0);
        PostEffect.SetFloat("_TitleLerpBlack", 0);
        _renderer.material.SetFloat("_Alpha", 1);

        finalRenderMat = new Material(renderMat.shader);
        finalRenderMat.SetTexture("_MainTex", img);
    }
    void OnEnable()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FadeOut(float duration) {
        TweenFactory.Tween("fadeTexture-" + System.Guid.NewGuid(), 0, 1, duration, TweenScaleFunctions.Linear, (v1) =>
        {
            //fix scene transition bug
            if(this == null) { return; }

            PostEffect.SetFloat("_TitleLerpWhite", Mathf.Pow(v1.CurrentValue, 4));
            _renderer.material.SetFloat("_Alpha", Mathf.Pow(Mathf.Abs(v1.CurrentValue-1),4));
        }, (v2) => {

        });
    }

    public void FadeToBlack(float duration)
    {
        TweenFactory.Tween("fadeTexture-" + System.Guid.NewGuid(), 1, 0, duration, TweenScaleFunctions.Linear, (v1) =>
        {
            PostEffect.SetFloat("_TitleLerpBlack", v1.CurrentValue);

        }, (v2) => {

        });
    }

    public void FadeToMask(float duration)
    {
        TweenFactory.Tween("fadeTexture-" + System.Guid.NewGuid(), 0, 1, duration, TweenScaleFunctions.Linear, (v1) =>
        {
            PostEffect.SetFloat("_TitleLerpBlack", v1.CurrentValue);

        }, (v2) => {

        });
    }
}
