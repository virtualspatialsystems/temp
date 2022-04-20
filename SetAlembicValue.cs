using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.Alembic;

public class SetAlembicValue : MonoBehaviour
{
    public float duration = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation() {
        float time = Convert.ToSingle(GetComponent<AlembicStreamPlayer>().endTime - GetComponent<AlembicStreamPlayer>().startTime);

        TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, time, duration, (float t) => {
            return t;
        }, (v1) =>
        {
            GetComponent<AlembicStreamPlayer>().currentTime = v1.CurrentValue;
        }, (v2) => {

        });
    }
}
