using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAmbienceZone : MonoBehaviour
{
    public GameObject audioGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera") {
            audioGroup.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            audioGroup.SetActive(false);
        }
    }

}
