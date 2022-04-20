using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using TMPro;
using UnityEngine.Playables;

[TrackBindingType(typeof(TextMeshPro))]
[TrackClipType(typeof(TranslationClip))]
public class TranslationTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<TranslationTrackMixer>.Create(graph, inputCount);
    }
}
