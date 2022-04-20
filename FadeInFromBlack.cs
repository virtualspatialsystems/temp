using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFromBlack : MonoBehaviour
{
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 1, 0, 2, TweenScaleFunctions.Linear, (v1) =>
        {

            sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, v1.CurrentValue);

        }, (v2) =>
        {

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
