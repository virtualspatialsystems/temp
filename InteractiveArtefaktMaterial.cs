using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
/*
 Changes the color an artefakt if the camera get`s close enough
     */
public class InteractiveArtefaktMaterial : MonoBehaviour
{

    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        foreach(ArtefaktTexture m in GetComponentsInChildren<ArtefaktTexture>())
        {
            m.SetLerpValue(1);
        }

        if (GetComponent<ArtefaktTexture>() != null) {
            GetComponent<ArtefaktTexture>().SetLerpValue(1);
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "MainCamera") {

            TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 1, 0, duration, TweenScaleFunctions.Linear, (v1) =>
            {
                foreach (ArtefaktTexture m in GetComponentsInChildren<ArtefaktTexture>())
                {
                    m.SetLerpValue( v1.CurrentValue);
                }

                

                if (GetComponent<ArtefaktTexture>() != null)
                {
                    GetComponent<ArtefaktTexture>().SetLerpValue(v1.CurrentValue);

                }

            }, (v2) =>
            {
                foreach (ArtefaktTexture m in GetComponentsInChildren<ArtefaktTexture>())
                {
                    //m.SetLerpValue(0);
                }
            });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 0, 1, duration, TweenScaleFunctions.Linear, (v1) =>
            {
                foreach (ArtefaktTexture m in GetComponentsInChildren<ArtefaktTexture>())
                {
                    m.SetLerpValue(v1.CurrentValue);
                }
                if (GetComponent<ArtefaktTexture>() != null)
                {
                    GetComponent<ArtefaktTexture>().SetLerpValue(v1.CurrentValue);

                }

            }, (v2) =>
            {
                foreach (ArtefaktTexture m in GetComponentsInChildren<ArtefaktTexture>())
                {
                    //m.SetLerpValue(1);
                }
            });
        }

        
    }
}
