using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWithExtras : MonoBehaviour
{
    public float duration = 5;
    public AudioClip audioClip;
    private bool targetReached = false;
    public bool startReached = false;
    public GameObject startTarget;
    public GameObject endTarget;
    

    public void Reached(GameObject agent, Action cb)
    {
        if (targetReached) { return; }
        targetReached = true;
        if (audioClip != null)
        {
            agent.GetComponent<AudioSource>().clip = audioClip;
            agent.GetComponent<AudioSource>().Play();

        }

        StartCoroutine(Delay(cb, duration));
    }

    public GameObject GetStartTarget()
    {
        return startTarget;
    }
    
    public GameObject GetEndTarget()
    {
        return endTarget;
    }

    IEnumerator Delay(Action cb, float time)
    {
        yield return new WaitForSeconds(time);

        cb?.Invoke();
    }
}
