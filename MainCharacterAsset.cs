using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MainCharacterAsset : PlayableAsset
{
    public GameObject character;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<MainCharacterBehaviour>.Create(graph);
        var mainCharacterBehaviour = playable.GetBehaviour();
        mainCharacterBehaviour.character = character;
        return playable;
    }
}
