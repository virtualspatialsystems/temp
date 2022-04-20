using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraHelperTrackBehaviour : PlayableBehaviour
{
#if UNITY_EDITOR
    GameObject _camera;
  GameObject mainCam
  {
    get
    {
      if(_camera == null)
      {

        GameObject _cam = GameObject.FindWithTag("debugCam");
        if(_cam == null) { 
          _camera = new GameObject("DebugCamera");
          _camera.AddComponent<Camera>();
          _camera.tag = "debugCam";
        }
        else
        {
          _camera = _cam;
        }

      }
      return _camera;
    }
  }

  private AlternativeLine _alternativeLine;
  private AlternativeLine alternativeLine
  {
    get
    {
      if(_alternativeLine == null)
      {
        AlternativeLine[] _allAlternativeLines = Resources.FindObjectsOfTypeAll(typeof(AlternativeLine)) as AlternativeLine[];
        
        if(_allAlternativeLines.Length > 0)
        {
          _alternativeLine = _allAlternativeLines[0];
        }
      }

      return _alternativeLine;
    }
  }

  private Director _director;
  private Director director {
    get
    {
      if(_director == null)
      {
        Director[] allDirectors = Resources.FindObjectsOfTypeAll(typeof(Director)) as Director[];
        
        if(allDirectors.Length > 0)
        {
          _director = allDirectors[0];
        }
      }
      return _director;
    }
  }
  public override void PrepareFrame(Playable playable, FrameData info)
  {
    base.PrepareFrame(playable, info);
    
    DirectorItem item = director.list.Find(t => t.timelines[0].name + ".PlayableDirector" == playable.GetGraph().GetEditorName());

    if(item != null)
    {
      float searchedTime = (float)playable.GetTime() - item.waitDuration;
      int _index = director.list.IndexOf(item);
      if(searchedTime > 0)
      {
        float alpha = searchedTime / item.durationToNextTarget;
        float realAlpha = item.movingInterpolation.Evaluate(alpha);

        Vector3 _pos = alternativeLine.GetPositionAtProgressFromLine(_index, realAlpha);

        _pos.y += 1.5f;
        mainCam.transform.position = _pos;
      }

    }
  }
#endif
}
