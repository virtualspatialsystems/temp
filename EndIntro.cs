using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndIntro : MonoBehaviour
{
    public VideoPlayer player;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMainScene()
    {
        director.Stop();
        player.Stop();
    }
}
