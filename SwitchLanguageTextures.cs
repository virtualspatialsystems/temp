using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguageTextures : MonoBehaviour
{
    public Material material;
    public Texture tex_DE;
    public Texture tex_EN;

    void Awake() {
        
        switch(StaticInformation.CurrentLanguage){
            case Language.DE:
                material.mainTexture = tex_DE;
            break;
            case Language.EN:
                material.mainTexture = tex_EN;
            break;
        }
    }

}
