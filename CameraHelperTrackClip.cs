using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraHelperTrackClip : PlayableAsset
{
  public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
  {
    var playable = ScriptPlayable<CameraHelperTrackBehaviour>.Create(graph);
    
    return playable;
    
  }
}
