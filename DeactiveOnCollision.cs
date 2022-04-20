using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveOnCollision : MonoBehaviour
{
    public BackgroundCharacterTexture BG;
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
            gameObject.SetActive(false);

    }
}
