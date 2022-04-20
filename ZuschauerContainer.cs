using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZuschauerContainer : MonoBehaviour
{
    public ZuschauerInformation[] infos;

    public Material ZuschauerMatFront;
    public Material ZuschauerMatBack;
    public GameObject lookAtTarget;
    public List<GameObject> InitInactive;


    // Start is called before the first frame update
    void Start()
    {
        //SetLookAt();
        SetTextures();
        foreach (GameObject g in InitInactive) {
            g.SetActive(false);
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLookAt() {
        Zuschauer[] zuschauer = GetComponentsInChildren<Zuschauer>();

        foreach (Zuschauer z in zuschauer) {
            z.SetLookAt(lookAtTarget);
        }
    }

    public void SetTextures() {
        Zuschauer[] zuschauer = GetComponentsInChildren<Zuschauer>();

        foreach (Zuschauer z in zuschauer) 
        {
            int idx = UnityEngine.Random.Range(0, infos.Length);
            z.SetTextures(ZuschauerMatFront, ZuschauerMatBack,infos[idx]);
        }
    }


    public void Applaus()
    {
        Zuschauer[] zuschauer = GetComponentsInChildren<Zuschauer>();

        foreach (Zuschauer z in zuschauer)
        {
            z.Applaus();
        }
    }

    public void NormalMovement() {
        Zuschauer[] zuschauer = GetComponentsInChildren<Zuschauer>();

        foreach (Zuschauer z in zuschauer)
        {
            z.NormalMovement();
        }
    }

    public void CalmMovement()
    {
        Zuschauer[] zuschauer = GetComponentsInChildren<Zuschauer>();

        foreach (Zuschauer z in zuschauer)
        {
            z.CalmMovement();
        }
    }

    public void ZuschauerDarker() {
        TweenFactory.Tween("transition-" + System.Guid.NewGuid(), 1, 0.02f, 8, TweenScaleFunctions.Linear, (v1) =>
        {

            ZuschauerMatFront.SetFloat("_ZuschauerStrength", v1.CurrentValue);
            ZuschauerMatBack.SetFloat("_ZuschauerStrength", v1.CurrentValue);

        }, (v2) =>
        {

        });
    }

    public void FadeOut(GameObject container)
    {

        Zuschauer[] zuschauer = container.GetComponentsInChildren<Zuschauer>();
        TweenFactory.Tween("transition-" + System.Guid.NewGuid(), 1,0,5, TweenScaleFunctions.Linear, (v1) =>
            {

                foreach(Zuschauer z in zuschauer)
                {
                    z.SetAlpha(v1.CurrentValue);
                }

            }, (v2) =>
            {
                container.SetActive(false);
            });
    }

    public void FadIn(GameObject container)
    {
        container.SetActive(true);
        Zuschauer[] zuschauer = container.GetComponentsInChildren<Zuschauer>();
        TweenFactory.Tween("transition-" + System.Guid.NewGuid(), 0, 1, 5, TweenScaleFunctions.Linear, (v1) =>
        {

            foreach (Zuschauer z in zuschauer)
            {
                z.SetAlpha(v1.CurrentValue);
            }

        }, (v2) =>
        {

        });
    }

}
