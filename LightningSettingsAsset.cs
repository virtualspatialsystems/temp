using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class LightningSettingsAsset : PlayableAsset
{
    public Color fogColor;
    public float fogDensity;

    public float maskSize;

    public Material PostMat;
    public float duration;

    public bool changeFog;
    public bool changeMaskSize;


    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<LightningSettingsBehaviour>.Create(graph);
        var lightSettingsBehaviour = playable.GetBehaviour();
        lightSettingsBehaviour.fogColor = fogColor;
        lightSettingsBehaviour.fogDensity = fogDensity;
        lightSettingsBehaviour.PostMat = PostMat;
        lightSettingsBehaviour.changeFog = changeFog;
        lightSettingsBehaviour.changeMaskSize = changeMaskSize;
        lightSettingsBehaviour.maskSize = maskSize;
        lightSettingsBehaviour.duration = duration;
        return playable;
    }
}
