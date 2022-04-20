using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPosition : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform.root.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localPosition = new Vector3(player.transform.position.x * -0.154f, 0, player.transform.position.z * -0.125f);
    }
}
