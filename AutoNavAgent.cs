using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AutoNavAgent : MonoBehaviour
{
    private bool hasStarted = false;
    public List<TargetWithExtras> goals;
    private int currentGoal = 0;
    private bool finish = false;
    public Action<Action> destinationReached;

    private AgentFollower _agentFollower;

    public Action intermediateDestinationReached;
    NavMeshAgent agent;

    public void Go(Action intermediateAction = null)
    {
        agent.destination = goals[currentGoal].startTarget == null ? goals[currentGoal].transform.position : goals[currentGoal].startTarget.transform.position;
       
        intermediateDestinationReached += intermediateAction;

        hasStarted = true;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
    }

    public Vector3 GetTargetPosition()
    {
        return goals[currentGoal].transform.position;
    }

    void HandleReachedDestination()
    {
        if (!goals[currentGoal].startReached && goals[currentGoal].startTarget != null )
        {
            goals[currentGoal].startReached = true;
            transform.position = goals[currentGoal].endTarget.transform.position;
            agent.destination = goals[currentGoal].gameObject.transform.position;

            intermediateDestinationReached?.Invoke();

            Debug.Log("reached intermediate");
            
        }
        else { 
            //Check for next goal
            int nextGoal = (currentGoal + 1 <= goals.Count - 1) ? currentGoal + 1 : 0;
       
            //Target reached
            goals[currentGoal].Reached(gameObject, () =>
            {
                if (goals.Count == 1 || nextGoal == 0)
                {
                    finish = true;
                }
                else
                {

                    Debug.Log("reached destination, nextTarget: " +" current" + currentGoal + " next"+ nextGoal +" " + goals[currentGoal].GetStartTarget());

                    agent.destination = goals[currentGoal].GetStartTarget() == null ? goals[nextGoal].gameObject.transform.position : goals[currentGoal].startTarget.transform.position;
               
                    currentGoal = nextGoal;
                }

            });
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

    void Update()
    {
        if (finish || !hasStarted) { return; }
            
        CheckForDestination(() =>
        {
            HandleReachedDestination();

        });
        

    }
}
