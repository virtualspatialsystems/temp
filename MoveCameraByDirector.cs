using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MoveCameraByDirector : MonoBehaviour
{

    public GameObject CameraToMove;

    public Action OnEnd;

    [SerializeField]
    private AlternativeLine alternativeLine;

    private List<Vector3> debugSpeedLinePoints = new List<Vector3>();

    [Header("Debug Settings")]
    [SerializeField, Tooltip("HelperBubblesteps alle N Sekunden"),Range(1f,20f)]
    private float _durationStep = 5f;

    [SerializeField, Tooltip("Größe der POIs Punkte zur Visualisierung"), Range(0.1f,3f)]
    private float _poiSize = .1f;

    private Director _directorScript;
    private Director _director {
        get { 
            if(_directorScript == null && this != null) //Fix scene transition bug
            {
                _directorScript = gameObject.GetComponent<Director>();
            }

            return _directorScript;
        } 
    }
    private int currentPOI = 0;
    FloatTween _currentTween;


    void GoToWayPoint(int POIIndex)
    {
        alternativeLine.GetPOIStartPosition(POIIndex);
    }
    public void JumpToPrevPOI()
    {
        StopCurrentPOI();

        if (currentPOI - 1 < 0) { return; }
        currentPOI = currentPOI - 1;
        CameraToMove.transform.position = alternativeLine.GetPOIStartPosition(currentPOI);
        TransitionToPOI();
    }

    public void JumpToNextPOI()
    {
        StopCurrentPOI();

        if (currentPOI + 1 > alternativeLine.GetAllPOIs().Count ) { Debug.Log(currentPOI ); Debug.Log(alternativeLine.GetAllPOIs().Count);  return;  }
        currentPOI = currentPOI + 1;
        CameraToMove.transform.position = alternativeLine.GetPOIStartPosition(currentPOI);

        TransitionToPOI();
    }

    private void StopCurrentPOI()
    {
        if (_currentTween != null)
        {
            _currentTween.Stop(TweenStopBehavior.DoNotModify);
        }

        _director.list[currentPOI].timelines.ForEach((PlayableDirector timeline) =>
        {
            timeline.Stop();

        });

    }

  float GetMeterPerSeconds(int _index, float percentageMargin, bool fromEnd)
  {

    var start = fromEnd ? 1 : 0;
    var end = fromEnd ? 1 - percentageMargin : percentageMargin;

    return GetMeterPerSecondsFromRange(_index, start, end);

  }


  float GetMeterPerSecondsFromRange(int _index, float start, float end)
  {
    Vector3 endPos = alternativeLine.GetPositionAtProgressFromLine(_index, start);
    Vector3 checkPos = alternativeLine.GetPositionAtProgressFromLine(_index, end);

    float checkDistance = Vector3.Distance(checkPos, endPos);
    float meterPerSek = 1f / (_director.list[_index].durationToNextTarget * .1f) * checkDistance;

    return meterPerSek;
  }

  Vector2 BezierCurve(float T, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
  {
      var t2 = T * T;
      var t3 = t2 * T;
      return a + (-a * 3 + T * (3 * a - a * T)) * T
      + (3 * b + T * (-6 * b + b * 3 * T)) * T
      + (c * 3 - c * 3 * T) * t2
      + d * t3;
  }

  public float GetBezier(List<Vector3> startPoints,float _alpha)
  {

    return BezierCurve(_alpha, startPoints[0], startPoints[1], startPoints[2], startPoints[3]).y;


  }


  private void Update()
  {
    if(debugSpeedLinePoints.Count < 2) { return; }
    for(int i=1;i< debugSpeedLinePoints.Count; i++)
    {
      Debug.DrawLine(debugSpeedLinePoints[i], debugSpeedLinePoints[i - 1], Color.red);

    }
      
  }

  public void TransitionToPOI()
  {
    float lastMeterPerSek = (currentPOI - 1 > 0) ? GetMeterPerSeconds(currentPOI - 1, .1f, true) : 0;
    float meterPerSek = GetMeterPerSeconds(currentPOI, .1f, true);


    Func<float, float> easingCurve = TweenScaleFunctions.Linear;

    Func<float, float> alternativeEasing = (float progress) => { return _director.list[currentPOI].movingInterpolation.Evaluate(progress); };

    Func<float, float> currentEasingCurve = _director.list[currentPOI].useAnimationCurve ? alternativeEasing : easingCurve;

    _director.list[currentPOI].timelines.ForEach((PlayableDirector timeline) =>
      {
          //bugfix for scene transitions
          if (this == null) { return; }

          timeline.Play();

      });


      _currentTween = TweenFactory.Tween("transition-" + System.Guid.NewGuid(), 0, 1, _director.list[currentPOI].durationToNextTarget, currentEasingCurve, (v1) =>
      { 
        //bugfix for scene transitions
        if (this == null) { return; }

        AnimationCurve _lastAnimCurve = currentPOI - 1 >= 0 ? _director.list[currentPOI - 1].movingInterpolation : null;

        if(_lastAnimCurve == null)
        {
        }

        CameraToMove.transform.position = alternativeLine.GetPositionAtProgressFromLine(currentPOI, v1.CurrentValue);

        float calculatedSpeed = GetMeterPerSecondsFromRange(currentPOI, v1.CurrentValue * .99f, v1.CurrentValue);
        
        debugSpeedLinePoints.Add(new Vector3(CameraToMove.transform.position.x, CameraToMove.transform.position.y + calculatedSpeed * 5f, CameraToMove.transform.position.z));

      }, (v2) =>
      {
          currentPOI = currentPOI + 1 < alternativeLine.GetAllPOIs().Count ? currentPOI + 1 : -1;

          if(currentPOI == -1)
          {
              Debug.Log("Ende erreicht");

              OnEnd?.Invoke();

              _director.list[_director.list.Count - 1].timelines.ForEach((PlayableDirector timeline) =>
              {
                  timeline.Play();

              });


              return;
          }

          TransitionToPOI();

      });

      _currentTween.Delay = _director.list[currentPOI].waitDuration;
  }


  void DrawSingleHelper(Vector3 POIPosition, DirectorItem _item, int index, DirectorItem _previousItem)
  {
    Vector3 _startPos = POIPosition;
     
    GUIStyle style = new GUIStyle();
    style.normal.textColor = Color.white;
    style.alignment = TextAnchor.MiddleCenter;
    float totalDistance = alternativeLine.GetLineLength(index);
    float averageSpeed = totalDistance / _item.durationToNextTarget;

    int counter = (int)(totalDistance / .3);
    float durationStep = _item.durationToNextTarget / counter;


    Vector3 _lastWayPoint = Vector3.zero;

    for (var i = 1; i <= counter; i++)
    {
      float checkDistanceAlpha = (1f / counter) * i;
      
      float alpha = 1f / counter * i;
      float beforeAlpha = 1f / counter * (i-1);

      float realAlpha = _item.movingInterpolation.Evaluate(alpha);
      float realBeforeAlpha = _item.movingInterpolation.Evaluate(beforeAlpha);
     
      Vector3 stepPos1 = alternativeLine.GetPositionAtProgressFromLine(index, realAlpha);
      Vector3 stepPos2 = alternativeLine.GetPositionAtProgressFromLine(index, realBeforeAlpha);
      Vector3 speed_Pos = alternativeLine.GetPositionAtProgressFromLine(index, checkDistanceAlpha);


      float distanceSpeed = Vector3.Magnitude( stepPos1 - stepPos2 );



      float speed = GetMeterPerSecondsFromRange(index, realAlpha, realAlpha * .9f);
      
      


      
      speed_Pos.y += speed * .60f;

      Color _speedColor = Color.HSVToRGB(1f - speed * .5f, 1, 1);
      Gizmos.color = _speedColor;// Color.red;
      Gizmos.DrawSphere(speed_Pos, .05f);

      if(i >= 2) { 
        Debug.DrawLine(_lastWayPoint, speed_Pos, Color.green);
        Debug.DrawLine(speed_Pos, _lastWayPoint, _speedColor);
      }

      _lastWayPoint = speed_Pos;
    }
      
#if UNITY_EDITOR
        Handles.color = Color.black;
        Gizmos.DrawSphere(_startPos, .1f);

        Handles.Label(_startPos + new Vector3(0, 3, 0), _item.directorItemName == "" ? "POI" : _item.directorItemName, style);
        Handles.Label(_startPos + new Vector3(0, 2, 0), _item._arriveAtPosition + "sek/" + _item._globalTime + "sek", style);
        Handles.Label(_startPos + new Vector3(0, 1, 0), "waitFor:" + _item.waitDuration + "sek", style);
        Handles.Label(_startPos + new Vector3(0, .9f, 0), "mustFadeSpeed:" + (_item.waitDuration == 0), style);

    bool mustFade = (_item.waitDuration == 0);
        if (mustFade) { 
            float lastMeterPerSek = (index - 1 >= 0) ? GetMeterPerSeconds(index - 1, .1f, true) : 0;
            float meterPerSek = GetMeterPerSeconds(index, .1f, true);


            Handles.Label(_startPos + new Vector3(0, .8f, 0), "last m/s:" + (lastMeterPerSek), style);
            Handles.Label(_startPos + new Vector3(0, .75f, 0), "m/s:" + (meterPerSek), style);
        }
        if (index < alternativeLine.GetAllPOIs().Count)
        {
            Vector3 _pos = alternativeLine.GetPositionAtProgressFromLine(index, .5f);
            Handles.Label(_pos + new Vector3(0, 1, 0), "transitionTime:" + _item.durationToNextTarget + "sek", style);
        }
#endif
    }


    void DrawHelper()
    {
      int directorItemIndex = 0;
      List<LineRenderer> _lines = alternativeLine.GetAllPOIs();
      List<GameObject> POIs = alternativeLine.POIs;

      for(int i = 0; i < POIs.Count - 1; i++)
      {
        DirectorItem lastItem = i - 1 >= 0 ? _director.list[i - 1] : null;
        DrawSingleHelper(POIs[i].transform.position, _director.list[i], i, lastItem);
      }
     
    }

    private void OnDrawGizmos()
    {
      #if UNITY_EDITOR
        DrawHelper();
          
    #endif
    }

}
