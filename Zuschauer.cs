using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zuschauer : MonoBehaviour
{
    
    public Texture _currentTextureFront;
    public Texture _currentTextureBack;

    private float yPosOffset;
    private float yPosSpeed;

    private float xPosOffset;
    private float xPosSpeed;

    private float alpha = 1;

    public Material _glowMat;

    private void Awake()
    {
        NormalMovement();
        

        CustomGlowObj _glowObj = GetComponent<CustomGlowObj>();
        if (_glowObj == null)
        {
            _glowObj = gameObject.AddComponent<CustomGlowObj>();
        }

        Material glow_mat = new Material(_glowMat.shader);
        glow_mat.SetTexture("_MainTex",_currentTextureFront);

        _glowObj.glowMaterial = glow_mat;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = transform.localScale * UnityEngine.Random.Range(0.95f, 1.05f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeMesh() {
      
    }

    public void Applaus()
    {
        NormalMovement();
        yPosOffset *= 1f;
        yPosSpeed *= 3;
        xPosSpeed *= 3;

        SetMaterialBlock();
        

       // _currentTexture = info.ApplausTexture;
    }

    public void NormalMovement() {
        xPosOffset = UnityEngine.Random.Range(0.0001f, 0.0003f);

        yPosSpeed = UnityEngine.Random.Range(0.1f, 1.0f);

        yPosOffset = UnityEngine.Random.Range(0.0001f, 0.0004f);

        xPosSpeed = UnityEngine.Random.Range(0.1f, 1.0f);

        SetMaterialBlock();
    }

    public void CalmMovement() {
        NormalMovement();
        yPosOffset *= 0.3f;
        xPosOffset *= 0.3f;
        yPosSpeed *= 0.5f;
        xPosSpeed *= 0.5f;

        SetMaterialBlock();
    }

    public void SetLookAt(GameObject target) {
        transform.parent.transform.LookAt(target.transform);

    }

    public void SetTextures(Material front, Material back, ZuschauerInformation info) {
        gameObject.GetComponent<MeshRenderer>().materials[0] = front;
        gameObject.GetComponent<MeshRenderer>().materials[1] = back;
        _currentTextureFront = info.IdleFrontTexture;
        _currentTextureBack = info.IdleBackTexture;

        SetMaterialBlock();
    }

    void SetMaterialBlock() {
        MaterialPropertyBlock _block = new MaterialPropertyBlock();
        GetComponentInChildren<MeshRenderer>().GetPropertyBlock(_block);
        _block.SetFloat("_yOffset", yPosOffset);
        _block.SetFloat("_yOffsetSpeed", yPosSpeed);
        _block.SetFloat("_xOffset", xPosOffset);
        _block.SetFloat("_xOffsetSpeed", xPosSpeed);
        _block.SetFloat("_Alpha", alpha);

        if (_currentTextureFront == null) {
            _currentTextureFront = GetComponent<MeshRenderer>().material.GetTexture("_MainTexture");
        }
        _block.SetTexture("_MainTexture", _currentTextureFront);
        
        GetComponentInChildren<MeshRenderer>().SetPropertyBlock(_block,0);

        if (_currentTextureBack == null)
        {
            _currentTextureBack = GetComponent<MeshRenderer>().material.GetTexture("_MainTexture");
        }
        _block.SetTexture("_MainTexture", _currentTextureBack);
        
        if (GetComponentInChildren<MeshRenderer>().materials.Length > 0) {

            GetComponentInChildren<MeshRenderer>().SetPropertyBlock(_block, 1);
        }
        

    }

    public void SetAlpha(float a) {
        alpha = a;
        SetMaterialBlock();
    }
}
