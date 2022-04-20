using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AgentFollower : MonoBehaviour
{
    private bool hasStarted = false;
    public GameObject agentToFollow;
    [Range(0f, 10f)]
    public float MinimumDistanceToAgent = 5f;

    private NavMeshAgent agent;

    public void Go()
    {
        agent.destination = agentToFollow.transform.position;
        hasStarted = true;
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!hasStarted) { return; }
        float distance = Vector3.Distance(transform.position, agentToFollow.transform.position);

        if (distance > MinimumDistanceToAgent)
        {
            if (agent.isStopped) { agent.isStopped = false; }
            agent.destination = agentToFollow.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}