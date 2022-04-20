using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RealworldImage : MonoBehaviour
{
    MaterialPropertyBlock _block;
    public Texture2D img;

    // Start is called before the first frame update
    void Start()
    {
        _block = new MaterialPropertyBlock();

        _block.SetTexture("_RealWorldImage", img);

        GetComponent<Renderer>().SetPropertyBlock(_block);
    }

}
