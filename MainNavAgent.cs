using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MainNavAgent : MonoBehaviour
{
    private bool hasStarted = false;
    public List<Target> goals;
    private int currentGoal = 0;
    private bool finish = false;
    public Action<Action> destinationReached;
    private AgentFollower _agentFollower;
    NavMeshAgent agent;

    public void Go()
    {
        Debug.Log("GOOOO to: " + currentGoal);
        agent.destination = goals[currentGoal].gameObject.transform.position;
        goals[currentGoal].Transition();
        hasStarted = true;

        _agentFollower.Go();
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goals[currentGoal].gameObject.transform.position;
    }

    void HandleReachedDestination()
    {
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
                agent.destination = goals[nextGoal].gameObject.transform.position;
                currentGoal = nextGoal;
            }

        });

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
        if (finish) { return; }
        CheckForDestination(() =>
        {
            HandleReachedDestination();

        });
        

    }
}
