using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class KeyboardControls : MonoBehaviour
{
    public string nextScene;
    public GameObject blackSphere;
    public float durationToBlack;
    public float durationToRestart = 5f;

    public UnityEvent OnNextScene;
    private bool isLoading = false;
    private float sleepDuration = 0;
    private Quaternion lastRotation;

    private CameraSphereFade _cameraSphereFade;

    private bool keyButtonDown = false;
    private float keyDownTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastRotation = Camera.main.transform.rotation;

        _cameraSphereFade = FindObjectOfType<CameraSphereFade>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("o"))
        {
            StaticInformation.CurrentScene = "Otto";
            LoadScene();
        }
        else if(Input.GetKey("w"))
        {
            StaticInformation.CurrentScene = "Walter";
            LoadScene();
        }
        else if(Input.GetKey("f"))
        {
            StaticInformation.CurrentScene = "Fritzi";
            LoadScene();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            
            keyButtonDown = true;
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Button pressed");
            

            keyButtonDown = true;
        }
        
        if(keyButtonDown){
            keyDownTime += Time.deltaTime;
        
        }
        
        if(keyDownTime > 5){
            StaticInformation.CurrentScene = "TitleScene";
            keyDownTime = 0;
            keyButtonDown = false;
            LoadScene();
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1") ){
            

            if(keyDownTime < 5 && keyButtonDown){
                LoadScene();
            }
            
            keyButtonDown = false;
            keyDownTime = 0;
        }
        
    }

    public void LoadScene()
    {

        if (!isLoading) {
            OnNextScene.Invoke();
            isLoading = true;

            Debug.Log("hit loadscene");

            _cameraSphereFade.FadeIn(() =>
            {
                StartCoroutine("LoadAsyncScene");
            });
            //DigitalRuby.Tween.TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, durationToBlack, (float t) =>
            //{
            //    return t;
            //}, (v1) =>
            //{
            //    blackSphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, v1.CurrentValue);
            //}, (v2) =>
            //{
            //});
        }
        

    }

    public void BackToMenu() {
        Debug.Log("Back to Menu");
        isLoading = true;
        nextScene = "TitleScene";
        StartCoroutine("LoadAsyncScene");
    }

    IEnumerator LoadAsyncScene()
    {
        
        if (nextScene == "MainScene") {
            nextScene = StaticInformation.CurrentScene != null ? StaticInformation.CurrentScene : "Walter";
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextScene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
