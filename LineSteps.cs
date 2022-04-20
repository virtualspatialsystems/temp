using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class LineSteps : MonoBehaviour
{
    public LineTiming[] _linetimings;
    
    private int _index = 0;

    private void Start()
    {

        _linetimings = GetComponentsInChildren<LineTiming>();

        Reset();
    }
    private void Reset()
    {
        for(int i=0;i<_linetimings.Length; i++)
        {
            _linetimings[i].ChangeProperty(0);
        }
    }
    public void BeginAnimation()
    {
        Reset();
        _index = 0;
        Tweener(_index);


    }

    void Tweener(int _indexObj)
    {
        float _time = _linetimings[_indexObj].time;
        TweenFactory.Tween("line_" + Guid.NewGuid(), 0f, 1f,_time, TweenScaleFunctions.CubicEaseIn, (v1) => {

            _linetimings[_indexObj].ChangeProperty(v1.CurrentValue);

        }, (v2) =>
        {
            if(_index + 1 < _linetimings.Length)
            {
                _index++;

                Tweener(_index);
            }
            else
            {
                Debug.Log("finished");
            }
        });
    }
}
