using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float strength;
    // Start is called before the first frame update
    void Start()
    {
        strength = Random.Range(0.3f,1.0f);

    }

}
