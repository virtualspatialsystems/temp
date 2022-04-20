using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DigitalRuby.Tween;

// A behaviour that is attached to a playable
public class LightningSettingsBehaviour : PlayableBehaviour
{
    public Color fogColor;
    public float fogDensity;
    public float duration;

    public float maskSize;

    public Material PostMat;


    public bool changeFog;
    public bool changeMaskSize;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {

    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (changeFog) {
            Color startColor = PostMat.GetColor("_FogColor");
            float startDensity = PostMat.GetFloat("_FogDensity");
            TweenFactory.Tween("fogTransition-" + System.Guid.NewGuid(), 0, 1, duration, (float t) =>
            {

                return t;
            }, (v1) =>
            {
                PostMat.SetColor("_FogColor", Color.Lerp(startColor, fogColor, v1.CurrentValue));
                PostMat.SetFloat("_FogDensity", Mathf.Lerp(startDensity, fogDensity, v1.CurrentValue));
            }, (v2) =>
            {
            });
        }

        if (changeMaskSize) {
            float startSize = PostMat.GetFloat("_GlobalMaskSize");
            TweenFactory.Tween("maskSizeTransition-" + System.Guid.NewGuid(), 0, 1, duration, (float t) =>
            {

                return t;
            }, (v1) =>
            {
                PostMat.SetFloat("_GlobalMaskSize", Mathf.Lerp(startSize, maskSize, v1.CurrentValue));
            }, (v2) =>
            {
            });
        }

        

    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }
}
