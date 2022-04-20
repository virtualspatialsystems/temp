using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using UnityEngine.SceneManagement;

public class GazeSelect : MonoBehaviour
{
    public Camera camera;
    RaycastHit[] hits;
    Transform lastHit;
    float timer = 0;
    public float gazeDuration;
    public GameObject gazePoint;
    private bool sceneStarted = false;
    public float durationToBlack = 1;
    public GameObject blackSphere;
    private bool hasHit = false;
    private Vector3 gazePos;

    void Start()
    {

        gazePoint.GetComponent<MeshRenderer>().enabled = false;
        
    }

    void Update()
    {
        if (!sceneStarted)
        {
            
            hits = Physics.RaycastAll(camera.transform.position, camera.transform.forward);
            hasHit = false;
            gazePos = new Vector3(100, 100, 100);
            for (int i = 0; i<hits.Length; i++) {
                if(hits[i].transform.tag == "MenuObject")
                {
                    hasHit = true;
                    gazePos = hits[i].point;
                }
            }
            gazePoint.transform.position = gazePos;

        }  
    }

   
}
