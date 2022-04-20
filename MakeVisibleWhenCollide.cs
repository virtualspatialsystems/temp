using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;



public class MakeVisibleWhenCollide : MonoBehaviour
{
    public GameObject objectToHide;
    public float duration = 1f;
    private MaterialPropertyBlock matBlock;
    private Renderer _renderer;
    private bool _inTransition = false;
    // Start is called before the first frame update
    void Awake()
    {
        _renderer = objectToHide.GetComponent<Renderer>();
        matBlock = new MaterialPropertyBlock();
        ToggleObject(0);
    }

    private void StartOpacity(int dir)
    {
        _inTransition = true;

        float start = dir > 0 ? 0 : 1;
        float end = dir > 0 ? 1 : 0;

        TweenFactory.Tween("opacity-" + System.Guid.NewGuid(),start, end, duration, TweenScaleFunctions.CubicEaseIn, (v1) =>
        {
            matBlock.SetFloat("_Opacity" , v1.CurrentValue);
            _renderer.SetPropertyBlock(matBlock);

        }, (v2) =>
        {
            _inTransition = false;

        });
    }

    private void ToggleObject(float _opacity)
    {
        matBlock.SetFloat("_Opacity", _opacity);
        _renderer.SetPropertyBlock(matBlock);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit");

        if (other.gameObject.CompareTag("MainCamera") && !_inTransition)
        {
            StartOpacity(1);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        StartOpacity(-1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
