using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;
public class LineStepTiming : MonoBehaviour
{
    public float _singleTime = 1f; 
    ChangeOffsetMaterialBlock[] _changeOffsetMaterialBlocks;
    private int current = 0;
    private void Start()
    {
        _changeOffsetMaterialBlocks = GetComponentsInChildren<ChangeOffsetMaterialBlock>();


        Reset();
    }

    private void Reset()
    {
        for(var i = 0; i < _changeOffsetMaterialBlocks.Length; i++)
        {
            _changeOffsetMaterialBlocks[i]._offset = 0;
        }
    }

    public void Animate(Action _cb=null)
    {
        TweenFactory.Tween("line-anim-" + Guid.NewGuid(), 0, 1, _singleTime, TweenScaleFunctions.CubicEaseIn, (v1) => {

            _changeOffsetMaterialBlocks[current]._offset = v1.CurrentValue;

        }, (v2) =>
        {

            if(current < _changeOffsetMaterialBlocks.Length - 1)
            {
                current++;
                Animate(_cb);
            }
            else
            {
                Debug.Log("finished");
                _cb?.Invoke();
            }


        });
    }

}
