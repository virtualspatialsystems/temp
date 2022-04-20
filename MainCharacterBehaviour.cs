using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DigitalRuby.Tween;

// A behaviour that is attached to a playable
public class MainCharacterBehaviour : PlayableBehaviour
{
    public GameObject character;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {
        character.SetActive(false);
    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {
        
    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        character.SetActive(true);
        TweenFactory.Tween("fadeIn-" + System.Guid.NewGuid(), 0, 1, 2, TweenScaleFunctions.Linear, (v1) =>
        {
            foreach (MeshRenderer r in character.GetComponentsInChildren<MeshRenderer>())
            {
                r.material.SetFloat("_Alpha", v1.CurrentValue);
            }

        }, (v2) =>
        {

        });
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        character.SetActive(false);
        TweenFactory.Tween("fadeOut-" + System.Guid.NewGuid(), 1, 0, 2, TweenScaleFunctions.Linear, (v1) =>
        {
            foreach (MeshRenderer r in character.GetComponentsInChildren<MeshRenderer>())
            {
                r.material.SetFloat("_Alpha", v1.CurrentValue);
            }

        }, (v2) =>
        {
            character.SetActive(false);
        });
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }
}
