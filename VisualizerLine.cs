using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class VisualizerLine : MonoBehaviour
{

    public GameObject MaskObject;
    public List<Line> _lineObjects;
    
    private int currentTarget = 0;
    private LookAt lookAt;


    void OnEnable()
    {
        lookAt = GetComponent<LookAt>();
        
        for(int i= _lineObjects.Count - 1; i>=0; i--)
        {
            _lineObjects[i]._lineAlpha = 0;

        }
    }

    public void GoToTarget(int targetIndex , Action cb= null)
    {
        Line currentLine = _lineObjects[targetIndex];
        lookAt.Place();

        TweenFactory.Tween("mover" + Guid.NewGuid(), 0, 1, 5f, TweenScaleFunctions.Linear, (v1) => {

            MaskObject.transform.position = currentLine.GetLastPoint();
            currentLine._lineAlpha = v1.CurrentValue;

        }, (v2) => {

            cb?.Invoke();
        });
    }

    public void GoToNextTarget(Action cb = null)
    {
        Line currentLine = _lineObjects[currentTarget];
        lookAt.Place();

        TweenFactory.Tween("mover_"+ Guid.NewGuid(), 0, 1, 5f, TweenScaleFunctions.Linear, (v1) => {

            MaskObject.transform.position = currentLine.GetLastPoint();
            currentLine._lineAlpha = v1.CurrentValue;

        }, (v2) => {

            cb?.Invoke();
        });
    }


}
