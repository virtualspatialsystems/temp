using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DigitalRuby.Tween;

public class AutoAgentFollower : MonoBehaviour
{
    private bool hasStarted = false;
    public NavMeshAgent agent;
    public List<TargetWithExtras> targetWithExtras;
    public Material MaskMaterial;

    private int currentGoal = 0;
    private bool finish = false;

    public void Go()
    {
        MaskMaterial.SetFloat("_Transparency", 0);

        hasStarted = true;

        if (targetWithExtras[currentGoal].startTarget == null)
        {
            agent.destination = targetWithExtras[currentGoal].transform.position;
            targetWithExtras[currentGoal].startReached = true;
        }
        else
        {
            agent.destination = targetWithExtras[currentGoal].startTarget.transform.position;
        }
    }

    void CheckForDestination(Action destinationReached)
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    destinationReached.Invoke();
                }
            }
        }
    }
    private void BlendIn()
    {
        TweenFactory.Tween("blender", 1f, 0, 1f, TweenScaleFunctions.Linear, (v1) =>
        {
            MaskMaterial.SetFloat("_Transparency", v1.CurrentValue);

        }, (v2) => { });
    }

    void MaskTransition(Action cb =null)
    {
        TweenFactory.Tween("blender", 0f, 1f, 1f, TweenScaleFunctions.Linear, (v1) => {
           
            MaskMaterial.SetFloat("_Transparency", v1.CurrentValue);

        }, (v2) => {
            agent.gameObject.transform.position = targetWithExtras[currentGoal].endTarget.transform.position;
            agent.destination = targetWithExtras[currentGoal].transform.position;
            transform.position = targetWithExtras[currentGoal].transform.position;
            targetWithExtras[currentGoal].startReached = true;


            BlendIn();
        });
    }

    void HandleReachedDestination()
    {
        if (!targetWithExtras[currentGoal].startReached)
        {
            MaskTransition();
        }
        else
        {

            //Check for next goal
            int nextGoal = (currentGoal + 1 <= targetWithExtras.Count - 1) ? currentGoal + 1 : 0;

            //Target reached
            targetWithExtras[currentGoal].Reached(gameObject, () =>
            {
                if (targetWithExtras.Count == 1 || nextGoal == 0)
                {
                    finish = true;
                }
                else
                {
                    //agent.destination = targetWithExtras[nextGoal].gameObject.transform.position;
                    currentGoal = nextGoal;

                    Go();
                }

            });

        }
    }


    void Update()
    {
        if (finish || !hasStarted) { return; }
        CheckForDestination(() =>
        {
            HandleReachedDestination();

        });


    }
}
