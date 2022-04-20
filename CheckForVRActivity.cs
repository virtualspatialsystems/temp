using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using Valve.VR;

public enum VRPlayerState
{
    ACTIVE,
    INACTIVE
}



public class CheckForVRActivity : MonoBehaviour
{
    public Action<VRPlayerState> OnUserStateChange;

    private List<Vector3> compareForwards;
    [Tooltip("Frames die sich die Brille nicht bewegt haben darf")]
    private int max = 900;
    private GameObject _cameraObject;

    public VRPlayerState playerState;


    private void Awake()
    {
        SceneManager.sceneLoaded -= Init;
        SceneManager.sceneLoaded += Init;

        OnUserStateChange -= StateChange;
    }

    private void StateChange(VRPlayerState state)
    {
        if(state == VRPlayerState.ACTIVE)
        {
            compareForwards.Clear();
        }
    }

    private void Init(Scene arg0, LoadSceneMode arg1)
    {
        compareForwards = new List<Vector3>();
        _cameraObject = GameObject.Find("CameraLeftEye").gameObject;
    }

    bool? IsUserPresent()
    {
        if(_cameraObject == null) { Debug.Log("no cameraObject in Scene"); return null;  }

        compareForwards.Add(_cameraObject.transform.forward);

        if(compareForwards.Count >= max)
        {
            compareForwards.RemoveAt(0);
            
            float compareValue = Mathf.Abs(Vector3.Dot(compareForwards[0], compareForwards[compareForwards.Count - 1]));
            float secondCompareValue = Mathf.Abs(Vector3.Dot(compareForwards[ (int)(compareForwards.Count / 2) ], compareForwards[compareForwards.Count - 1]));

            if (compareValue >= .999999f && secondCompareValue >= .999999f) 
            {
                return false;
            }
        }
        else
        {
            return null;
        }

        return true;
    }

    private void Update()
    {
        bool? _isPresent = IsUserPresent();

        if(_isPresent == null) { return; }

        if (_isPresent == false && playerState == VRPlayerState.ACTIVE)
        {
            playerState = VRPlayerState.INACTIVE;

            if(SceneManager.GetActiveScene().name != "TitleScene")
            {
                SceneManager.LoadScene("TitleScene",LoadSceneMode.Single);
            }

            OnUserStateChange?.Invoke(playerState);

        }
        else if(_isPresent == true && playerState == VRPlayerState.INACTIVE)
        {
            playerState = VRPlayerState.ACTIVE;
            OnUserStateChange?.Invoke(playerState);
        }
           
    }
}
