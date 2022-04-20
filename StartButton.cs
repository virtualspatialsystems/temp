using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public string mainSceneToLoad = "Walter";
    public string nextScene = "Intro";
    public int loadingDuration = 3;
    public Image image;

    public bool changeSubtitle = false;
    public bool subtitleEnabled;
    
    private bool isLoading = false;
    private float time = 0.0f;
    public GameObject blackSphere;
    public float durationToBlack = 1;
    private bool sceneIsLoading = false;


    // Start is called before the first frame update
    void Start()
    {
        image.fillAmount = 0;

        if(!changeSubtitle){
            subtitleEnabled = StaticInformation.SubtitleEnabled;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isLoading) {
            time += Time.deltaTime;
            image.fillAmount = time/loadingDuration;
            if (time >= loadingDuration) {
                isLoading = false;
                LoadScene();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GazePoint") {
            time = 0;
            isLoading = true;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GazePoint")
        {
            image.fillAmount = 0;
            isLoading = false;
            time = 0;
        }
    }

    public void LoadScene() {
        if (!sceneIsLoading) {
            sceneIsLoading = true;
            TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, durationToBlack, (float t) =>
            {
                return t;
            }, (v1) =>
            {
                if (blackSphere != null) {
                    blackSphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, v1.CurrentValue);
                }
                
            }, (v2) =>
            {
                //fix scene transition bug
                if(this == null) { return; }
                StartCoroutine("LoadAsyncScene");
            });
        }

    }

    IEnumerator LoadAsyncScene()
    {

        StaticInformation.CurrentScene = mainSceneToLoad;

        StaticInformation.SubtitleEnabled = subtitleEnabled;

        //set Language zu English sobald der Subtitle an ist. Muss geändert werden sobald mehr als eine Sprache vorhanden ist
        StaticInformation.CurrentLanguage = subtitleEnabled ? Language.EN : Language.DE;

        Debug.Log(StaticInformation.SubtitleEnabled);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
