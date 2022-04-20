using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public List<Texture2D> images;
    public float lerpDuration = 1;
    public float stillDuration = 2;

    private Renderer rend;
    private string currentTexture = "_TextureA";
    private int currentIdx = 1;
    private int start = 0;
    private int end = 1;

    private void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material.SetTexture("_TextureA", images[0]);
        rend.material.SetTexture("_TextureB", images[1]);

        DigitalRuby.Tween.TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, stillDuration, (float t) =>
        {
            return t;
        }, (v1) =>
        {


        }, (v2) =>
        {
            DigitalRuby.Tween.TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, lerpDuration, (float t) =>
            {
                return t;
            }, (v3) =>
            {
                if (rend == null) { return; }

                rend.material.SetFloat("_Lerp", v3.CurrentValue);
            }, (v4) =>
            {
                ChangeTexture();
            });
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeTexture() {

        //fix scene transition bug
        if(rend == null) { return; }

        currentIdx = currentIdx == images.Count - 1 ? 0 : currentIdx+1;
        rend.material.SetTexture(currentTexture, images[currentIdx]);

        currentTexture = currentTexture == "_TextureA" ? "_TextureB" : "_TextureA";
        start = currentTexture == "_TextureA" ? 0 : 1;
        end = currentTexture == "_TextureA" ? 1 : 0;

        DigitalRuby.Tween.TweenFactory.Tween("black-" + System.Guid.NewGuid(), start, end, stillDuration, (float t) =>
        {
            return t;
        }, (v1) =>
        {


        }, (v2) =>
        {
            DigitalRuby.Tween.TweenFactory.Tween("black-" + System.Guid.NewGuid(), start, end, lerpDuration, (float t) =>
            {
                return t;
            }, (v3) =>
            {
                if(rend == null) { return; }

                rend.material.SetFloat("_Lerp", v3.CurrentValue);
            }, (v4) =>
            {
                ChangeTexture();
            });
        });
    }


}
