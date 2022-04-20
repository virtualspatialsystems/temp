using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour
{
    public GameObject active;
    public GameObject inactive;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        active.SetActive(false);
        inactive.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GazePoint")
        {
            active.SetActive(true);
            inactive.SetActive(false);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "GazePoint")
        {
            active.SetActive(false);
            inactive.SetActive(true);

        }
    }
}
