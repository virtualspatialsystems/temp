using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using System;

public class CameraSphereFade : MonoBehaviour
{
    public float durationToBlack = 5f;

    private Renderer _renderer;
    private bool isBlack = false;
    private CheckForVRActivity _checkForVRActivity;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _checkForVRActivity = GameObject.FindObjectOfType<CheckForVRActivity>();
        if(_checkForVRActivity == null) { return; }
        _checkForVRActivity.OnUserStateChange -= HandleVRPlayerActivity;
        _checkForVRActivity.OnUserStateChange += HandleVRPlayerActivity;

    }

    private void HandleVRPlayerActivity(VRPlayerState _playerState)
    {        
        if(isBlack && _playerState == VRPlayerState.INACTIVE) { return; }
        if(!isBlack && _playerState == VRPlayerState.ACTIVE) { return; }

        if(_playerState == VRPlayerState.INACTIVE)
        {
            FadeInFast();
        }
        else
        {
            FadeOutFast();
        }
    }

    private void OnDisable()
    {
        if(_checkForVRActivity == null) { return; }
        _checkForVRActivity.OnUserStateChange -= HandleVRPlayerActivity;
    }

    public void FadeIn(Action OnComplete = null)
    {
        if (isBlack) { return; }

        TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, durationToBlack, (float t) =>
        {
            return t;
        }, (v1) =>
        {
            _renderer.material.color = new Color(0, 0, 0, v1.CurrentValue);
        }, (v2) =>
        {
            isBlack = true;
            OnComplete?.Invoke();
        });
    }

    public void FadeOut(Action OnComplete = null)
    {
        if (!isBlack) { return; }
        TweenFactory.Tween("black-" + System.Guid.NewGuid(), 1, 0, durationToBlack, (float t) =>
        {
            return t;
        }, (v1) =>
        {
            _renderer.material.color = new Color(0, 0, 0, v1.CurrentValue);
        }, (v2) =>
        {

            isBlack = false;
            OnComplete?.Invoke();
        });
    }

    public void FadeOutFast()
    {
         isBlack = false;
         _renderer.material.color = new Color(0, 0, 0, 0);
        
    }
    
    public void FadeInFast()
    {
         isBlack = true;
        _renderer.material.color = new Color(0, 0, 0, 1);
    }



}
