using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;



public class TranslationTrackMixer : PlayableBehaviour
{

    
    Language currentLanguage = Language.EN;
 
    private int current = 0;

    public string currentText = "";

    float duration = 0;
    float currentTime = 0;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
       

        TextMeshPro text = playerData as TextMeshPro;
        if(!text) { return; }

        if(Application.isPlaying){
            //Falls subtitles nicht aktiv sind wird der gesamte Textblock disabled 
            if(!StaticInformation.SubtitleEnabled){

                text.gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
                return;
            }else if(
                StaticInformation.SubtitleEnabled ){
//
                text.gameObject.transform.parent.transform.parent.gameObject.SetActive(true);
            }
        }
        

        string currentText = "";
        float currentAlpha = 0f;

        int inputCount = playable.GetInputCount();

        

        for(int i=0;i<inputCount;i++){
            float inputWeight = playable.GetInputWeight(i);
            ScriptPlayable<TranslationBehaviour> inputPlayable = (ScriptPlayable<TranslationBehaviour>)playable.GetInput(i);
             
            if(inputWeight > 0f){
                TranslationBehaviour clip = inputPlayable.GetBehaviour();

                duration = clip.duration;
                currentTime = clip.currentTime;

                //Get Current Language String object
                TranslationObject _translation =  clip.translations.Find(translation => translation.language == currentLanguage);
                string[] textPieces = _translation.text.Split('#');

                //Anzahl der Abschnitte die in einem Clip abgespielt werden sollen
                float abschnitteLength = duration / (float)textPieces.Length;
            
                current = Mathf.FloorToInt(currentTime / abschnitteLength);

                //Boundary
                current = current > textPieces.Length - 1 ? textPieces.Length - 1 : current;
                current = current < 0 ? 0 : current;
                
                //Set Text und Alpha
                currentText = textPieces[current];
                currentAlpha = inputWeight;
            }
        }

        text.text = currentText;
        text.color = new Color(text.color.r, text.color.g, text.color.b, currentAlpha);
        
    }
}
