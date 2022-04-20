using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefaktTexture : MonoBehaviour
{
    MaterialPropertyBlock _block;
    public Texture2D img;
    public Texture2D normals;
    public Texture2D metalic;
    public Texture2D roughness;
    public float lerpValue = 0;
    public float step1 = 0.15f;
    public float step2 = 0.65f;

    // Start is called before the first frame update
    void Awake()
    {
        if (metalic == null) {
            metalic = Texture2D.blackTexture;
        }

        if (roughness == null) {
            roughness = Texture2D.blackTexture;
        }

        if (normals == null)
        {
            normals = Texture2D.blackTexture;
        }

        _block = new MaterialPropertyBlock();
        GetComponent<Renderer>().GetPropertyBlock(_block, GetComponent<Renderer>().materials.Length-1);
        lerpValue = 1;
        _block.SetTexture("_ArtefaktTexture", img);
        _block.SetTexture("_NormalMapArtefakte", normals);
        _block.SetTexture("_MetalnessMapArtefakte", metalic);
        _block.SetTexture("_RoughnessMapArtefakte", roughness);
        _block.SetFloat("_LerpTexture", lerpValue);
        _block.SetFloat("_Step1", step1);
        _block.SetFloat("_Step2", step2);

        GetComponent<Renderer>().SetPropertyBlock(_block, GetComponent<Renderer>().materials.Length - 1);
    }


    public void SetLerpValue(float l) {
        lerpValue = l;
        _block = new MaterialPropertyBlock();
        GetComponent<Renderer>().GetPropertyBlock(_block, GetComponent<Renderer>().materials.Length - 1);
        _block.SetTexture("_ArtefaktTexture", img);
        _block.SetTexture("_NormalMapArtefakte", normals);
        _block.SetTexture("_MetalnessMapArtefakte", metalic);
        _block.SetTexture("_RoughnessMapArtefakte", roughness);
        _block.SetFloat("_LerpTexture", lerpValue);
        _block.SetFloat("_Step1", step1);
        _block.SetFloat("_Step2", step2);

        GetComponent<Renderer>().SetPropertyBlock(_block, GetComponent<Renderer>().materials.Length - 1);
    }

    void Update()
    {
    }

}
