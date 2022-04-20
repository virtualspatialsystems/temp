using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

//[ExecuteAlways]
public class MainCharacter : MonoBehaviour
{
    private float durationFadeIn = 0.5f;
    private float durationFadeOut = 1.0f;
    //public Material _glowMat;
    //public Texture2D img;
    MaterialPropertyBlock _propertyBlock;
    Renderer _renderer;

    private void Awake()
    {
        /*_glowMat = Resources.Load<Material>("Glow");
        img = Resources.Load<Texture2D>("default_texture");
        

        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            CustomGlowObj _glowObj = r.gameObject.GetComponent<CustomGlowObj>();
            if (_glowObj == null)
            {
                _glowObj = r.gameObject.AddComponent<CustomGlowObj>();
            }

            Material glow_mat = new Material(_glowMat.shader);
            glow_mat.SetTexture("_MainTex", img);

            _glowObj.glowMaterial = glow_mat;
        }*/
    }
    // Start is called before the first frame update
    void SetPositionInShader()
    {       
        _renderer = GetComponent<Renderer>();
        if(_renderer == null){return;}
        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetVector("_WorldPositionInScene", transform.position);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    void Start(){
        SetPositionInShader();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        TweenFactory.Tween("fadeIn-" + System.Guid.NewGuid(), 0, 1, durationFadeIn, TweenScaleFunctions.Linear, (v1) =>
        {
            //fix scene transition bug
            if(this == null) { return; }
            foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>()) {
                r.material.SetFloat("_Alpha", v1.CurrentValue);
            }

        }, (v2) =>
        {

        });
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "MainCamera")
             FadeOut();
    }

    public void FadeOut() {
        TweenFactory.Tween("fadeOut-" + System.Guid.NewGuid(), 1, 0, durationFadeOut, TweenScaleFunctions.Linear, (v1) =>
        {
            //fix scene transition bug
            if (this == null) { return; }
            foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
            {
                r.material.SetFloat("_Alpha", v1.CurrentValue);
            }

        }, (v2) =>
        {
            //gameObject.SetActive(false);
        });
    }
    
}
