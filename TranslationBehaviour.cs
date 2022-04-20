using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

public class TranslationBehaviour : PlayableBehaviour
{
    public string translationName;
    public List<TranslationObject> translations;
    
    [HideInInspector]
    public float duration;

    [HideInInspector]
    public float currentTime;


    private int current = 0;

    public string currentText = "";
    Language currentLanguage = Language.DE;


    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);

        duration = (float)playable.GetDuration();       
        currentTime = (float)playable.GetTime(); 


    }


}
