using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;
using UnityEditor;
using System.IO;

//[ExecuteAlways]
public class Director : MonoBehaviour
{
    public AlternativeLine mainCameraLine;

    [HideInInspector]
    public List<DirectorItem> list = new List<DirectorItem>();
    //private List<GameObject> debug_visualIndicator = new List<GameObject>();

    //private FloatTween _currentTween;
    //private int _currentIndex = 0;

    private MoveCameraByDirector _moveCameraByDirector;

    private void Start()
    {
        _moveCameraByDirector = gameObject.GetComponent<MoveCameraByDirector>();

        _moveCameraByDirector.TransitionToPOI();
        //PlayDirectorItem(_currentIndex);
    }
    
  public AnimationCurve TransitionToLastPOI(int lastIndex)
  {
    var lastSteepPoint1 = list[lastIndex].movingInterpolation.Evaluate(1);
    Keyframe[] keyframes = list[lastIndex + 1].movingInterpolation.keys;
    keyframes[0] = new Keyframe(0, lastSteepPoint1);
    return new AnimationCurve(keyframes);
  }
}
