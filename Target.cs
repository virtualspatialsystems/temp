using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Target : MonoBehaviour
{
    public float duration = 5;
    public AudioClip audioClip;
    private bool targetReached = false;
    public PlayableDirector director;
    public PlayableAsset timelineTransition;
    public PlayableAsset timelineReached;


    public void Reached()
    {
        if (targetReached) { return; }
        targetReached = true;

        if (timelineReached != null)
        {
            director.time = 0;
            director.Play(timelineReached);
        }

    }

    public void Reached(GameObject agent, Action cb)
    {
        if (targetReached) { return; }
        targetReached = true;
        if (audioClip != null)
        {
            agent.GetComponent<AudioSource>().clip = audioClip;
            agent.GetComponent<AudioSource>().Play();

        }
        if (timelineReached != null)
        {
            director.time = 0;
            director.Play(timelineReached);
        }


        StartCoroutine(Delay(cb, duration));
    }

    IEnumerator Delay(Action cb, float time)
    {
        yield return new WaitForSeconds(time);

        cb?.Invoke();
    }

    public void Transition()
    {
        if (timelineTransition != null)
        {
            director.time = 0;
            director.Play(timelineTransition);
        }

    }
}
