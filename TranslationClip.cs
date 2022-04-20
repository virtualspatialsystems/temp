using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TranslationClip : PlayableAsset
{
    public string TranslationName;

    public List<TranslationObject> translations = new List<TranslationObject>();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TranslationBehaviour>.Create(graph);

        TranslationBehaviour translationBehaviour = playable.GetBehaviour();
        translationBehaviour.translations = translations;
        return playable;
        
    }

}
