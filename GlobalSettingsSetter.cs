using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingsSetter : MonoBehaviour
{
    [SerializeField]
    public GlobalSettings settings;

    // Start is called before the first frame update
    void Awake()
    {
        Shader.SetGlobalColor("DarkToonColor", settings.DarkToonColor);
        Shader.SetGlobalColor("MidToonColor", settings.MidToonColor);
        Shader.SetGlobalColor("LightToonColor", settings.LightToonColor);
        Shader.SetGlobalColor("DarkToonColorOutside", settings.DarkToonColorOutside);
        Shader.SetGlobalColor("MidToonColorOutside", settings.MidToonColorOutside);
        Shader.SetGlobalColor("LightToonColorOutside", settings.LightToonColorOutside);
        Shader.SetGlobalColor("BackgroundCharacterColor", settings.BackgroundCharacter);
        Shader.SetGlobalTexture("PaperBack", settings.PaperBack);
        Shader.SetGlobalFloat("CharacterAlpha", 1);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeOutAllCharacters() {

        TweenFactory.Tween("black-" + System.Guid.NewGuid(), 1, 0, 5, (float t) => {
            return t;
        }, (v1) =>
        {
            Shader.SetGlobalFloat("CharacterAlpha", v1.CurrentValue);
        }, (v2) => {

        });
    }
}
