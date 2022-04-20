using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;


public class RandomActivation : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration = 2;
    private bool stop = false;
    public float lightStrength = 3;
    private float lightStartStrength;
    public Material startMat;
    private BillboardPlane[] _billboardPlanes;

    void Start()
    {
        _billboardPlanes = GameObject.FindObjectsOfType<BillboardPlane>();

        lightStartStrength = startMat.color.a;
        CompleteFadeOut();

    }

    // Update is called once per frame
    void Update()
    {
 
    }

    void ChangeCurrent()
    {
        int count = transform.childCount;
        int idx = Random.Range(0, count - 2);
        bool isLast = false;

        if (stop)
            isLast = true;

        if (isLast)
        {
            idx = count - 1;
        }

        TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 0, 0.6f, duration, TweenScaleFunctions.Linear, (v1) =>
         {
             Renderer[] renderers = gameObject.transform.GetChild(idx).GetComponentsInChildren<Renderer>();
             foreach (Renderer r in renderers)
             {
                 r.material.color = new Color(1, 1, 1, v1.CurrentValue);
             }
         }, (v2) =>
         {
             if (!isLast)
             {
                 TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 0.6f, 0, duration, TweenScaleFunctions.Linear, (v3) =>
                 {
                     Renderer[] renderers = gameObject.transform.GetChild(idx).GetComponentsInChildren<Renderer>();
                     foreach (Renderer r in renderers)
                     {
                         r.material.color = new Color(1, 1, 1, v3.CurrentValue);
                     }
                 }, (v4) =>
                 {
                     if (!isLast)
                     {
                         ChangeCurrent();
                     }
                 });
             }


         });
    }

    public void StartAnimation()
    {
        stop = false;
        ChangeCurrent();
    }

    public void ShowLast()
    {
        stop = true;
    }

    public void ShowAll()
    {
        stop = true;
        TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 0, lightStartStrength, duration, TweenScaleFunctions.Linear, (v1) =>
        {
            foreach (Transform cluster in gameObject.GetComponentInChildren<Transform>())
            {
                Renderer[] renderers = cluster.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = new Color(1, 1, 1, v1.CurrentValue);
                }
            }

        }, (v2) =>
        {

        });
    }

    public void CompleteFadeOut()
    {
        TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), lightStartStrength, 0, duration, TweenScaleFunctions.Linear, (v1) =>
        {
            foreach (Transform cluster in gameObject.GetComponentInChildren<Transform>())
            {
                Renderer[] renderers = cluster.GetComponentsInChildren<Renderer>();
                foreach (Renderer r in renderers)
                {
                    r.material.color = new Color(1, 1, 1, v1.CurrentValue);
                }
            }

        }, (v2) =>
        {

        });
    }



    public void fadeOutLast()
    {
        TweenFactory.Tween("lerpTexture-" + System.Guid.NewGuid(), 0.6f, 0, duration, TweenScaleFunctions.Linear, (v3) =>
        {
            Renderer[] renderers = gameObject.transform.GetChild(transform.childCount-1).GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderers)
            {
                r.material.color = new Color(1, 1, 1, v3.CurrentValue);
            }
        }, (v4) =>
        {

        });
    }
}
