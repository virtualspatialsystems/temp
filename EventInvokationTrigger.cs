using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventInvokationTrigger : MonoBehaviour {

    public bool EventInvokationTriggerActive = true;

    [Header("Leave empty detect all collisions")]
    public string _colliderTag;
    
    [Space(5)]
    public UnityEvent OnTriggerEnterEvent;
    [Space(5)]
    public UnityEvent OnTriggerStayEvent;
    [Space(5)]
    public UnityEvent OnTriggerExitEvent;

    void Start () {
        if (_colliderTag == null)
        {
            _colliderTag = "other";
        }
    }
	
	private void OnTriggerEnter(Collider Collider)

    {
        if (_colliderTag == Collider.tag)
            OnTriggerEnterEvent.Invoke();
    }
        
    private void OnTriggerStay(Collider Collider)
    {
        if (_colliderTag == Collider.tag)
            OnTriggerStayEvent.Invoke();
    }

    private void OnTriggerExit(Collider Collider)
    {
        if (_colliderTag == Collider.tag)
            OnTriggerExitEvent.Invoke();
    }

}
