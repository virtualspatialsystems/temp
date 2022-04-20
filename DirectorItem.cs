using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[Serializable]
public class DirectorItem
{
    public string directorItemName;
    public float Progress;
    public List<PlayableDirector> timelines = new List<PlayableDirector>();
    public float waitDuration;
    public float durationToNextTarget;
    public bool useAnimationCurve;
    public AnimationCurve movingInterpolation;
    public int _indexInGlobalList;

    public float _arriveAtPosition;
    public float _globalTime;


}
