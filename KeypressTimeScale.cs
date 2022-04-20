using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypressTimeScale : MonoBehaviour
{
    public KeyCode TimeButton = KeyCode.T;
    public float AlternateTimeScale = 3.0f;
    private float fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime, it defaults to 0.02f, but it can be changed in the editor
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    void Update()
    {
        if (Input.GetKeyDown(TimeButton))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = AlternateTimeScale;
            else
                Time.timeScale = 1.0f;
            // Adjust fixed delta time according to timescale
            // The fixed delta time will now be 0.02 frames per real-time second
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }
}
