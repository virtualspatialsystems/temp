using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCredits : MonoBehaviour
{
    public GameObject germanCredits;
    public GameObject englishCredits;
    // Start is called before the first frame update
    void Awake()
    {
        englishCredits.SetActive(StaticInformation.SubtitleEnabled);
        germanCredits.SetActive(!StaticInformation.SubtitleEnabled);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
