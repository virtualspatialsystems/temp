using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    //public GameObject blackSphere;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            LoadMenu();
        }
    }

    public void LoadMenu()
    {
        /*TweenFactory.Tween("black-" + System.Guid.NewGuid(), 0, 1, 2, (float t) => {
            return t;
        }, (v1) =>
        {
            blackSphere.GetComponent<Renderer>().material.color = new Color(0, 0, 0, v1.CurrentValue);
        }, (v2) => {
            
        });*/
        StartCoroutine("LoadAsyncScene");
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StartMenu");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
