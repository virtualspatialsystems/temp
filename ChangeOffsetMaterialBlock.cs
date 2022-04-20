using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

[ExecuteAlways]
public class ChangeOffsetMaterialBlock : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    public string Attribute = "_Offset";
    [Range(0,1)]
    public float _offset = 1f;
    public float _duration = 1f;
    public Action _cb;
    public bool _respectMeshSize = false;
    private int vertCount = 0;

    void OnEnable()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
        MeshFilter _filter = GetComponent<MeshFilter>();
        vertCount = _filter.sharedMesh.vertexCount;

    }

    void Update()
    {
        // Get the current value of the material properties in the renderer.
        _renderer.GetPropertyBlock(_propBlock);

        _propBlock.SetFloat("_Offset", _offset);

        _renderer.SetPropertyBlock(_propBlock);
    }

    public void Animate()
    {
        float _vertexDuration = _respectMeshSize ? (vertCount / 100) : 1;
        TweenFactory.Tween("lerper_"+ Guid.NewGuid(), 0, 1, _duration * _vertexDuration, TweenScaleFunctions.CubicEaseIn, (v1) => {
            _offset = v1.CurrentValue;
        }, (v2) => {
            _cb?.Invoke();
        });
    }
}
