using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMap : MonoBehaviour
{

    public Material lowerFloor;
    public Material upperFloor;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 4.5)
        {
            gameObject.GetComponent<MeshRenderer>().material = lowerFloor;
        }
        else {
            gameObject.GetComponent<MeshRenderer>().material = upperFloor;
        }
    }
}
