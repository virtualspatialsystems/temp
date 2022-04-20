using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;


public class JumpTo : MonoBehaviour
{
    private int currentTarget = 0;
    public List<Target> targets;
    public GameObject Player;
    public GameObject Protagonist;
    public Material Blender;

    
    public void JumpToTarget(Transform targetPosition)
    {
        Player.transform.position = targetPosition.position;
        Player.transform.rotation = targetPosition.rotation;

        Protagonist.transform.position = targetPosition.position + new Vector3(2f, 0, 0);
        Protagonist.transform.rotation = targetPosition.rotation * Quaternion.Euler(0,-90f,0);
        
    }

    private void BlendIn()
    {
        TweenFactory.Tween("blender", 1f, 0, 1f, TweenScaleFunctions.Linear, (v1) =>
        {
            Blender.SetFloat("_Transparency", v1.CurrentValue);

        }, (v2) => { });
    }

    public void JumpToNextTarget()
    {

        TweenFactory.Tween("blender", 0, 1, 1f, TweenScaleFunctions.Linear, (v1)=> {
            Debug.Log("tween " + v1.CurrentValue);
            Blender.SetFloat("_Transparency", v1.CurrentValue);

        }, (v2)=> {
            JumpToTarget(targets[currentTarget].gameObject.transform);
            currentTarget = currentTarget + 1 < targets.Count ? currentTarget + 1: 0;
            BlendIn();
        });
 
       
    }

     

}
