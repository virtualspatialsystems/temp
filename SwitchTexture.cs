using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTexture : MonoBehaviour
{
    public List<Material> materials;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().material = materials[Random.Range(0, materials.Count-1)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
