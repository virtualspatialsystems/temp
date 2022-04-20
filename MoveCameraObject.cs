
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraObject : MonoBehaviour
{
    public Line line;

    [Range(0,1)]

    public float lerpAlpha = 0;
    private float lastAlpha = -1;

    private void Start()
    {
        SetLerpAlpha(0);
    }

    void SetLerpAlpha(float alpha)
    {
        line._lineAlpha = alpha;
        transform.position = line.GetPositionAtProgress(alpha);
        lastAlpha = alpha;
    }


    private void Update()
    {
        if(lerpAlpha != lastAlpha)
        {
            SetLerpAlpha(lerpAlpha);
        }
    }
}
