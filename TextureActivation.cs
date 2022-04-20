using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TextureActivation : MonoBehaviour
{
    public LayerMask layerMask;
    public GameObject texture;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics.Raycast(transform.position, transform.forward, 100, layerMask.value);
        texture.SetActive(hit);
    }
}
