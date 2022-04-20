using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMultipleMaterialBlocks : MonoBehaviour
{
    private ChangeOffsetMaterialBlock[] _materialBlocks;
    public bool _reverse = false;
    public float _duration = 1f;
    private void Start()
    {
        Reset();

    }

    private void Reset()
    {
        GetChildBlocks();
    }

    void GetChildBlocks()
    {
        _materialBlocks = GetComponentsInChildren<ChangeOffsetMaterialBlock>();

        if (_reverse)
        {
            System.Array.Reverse(_materialBlocks);
        }

        for (int i = 0; i < _materialBlocks.Length; i++)
        {
            _materialBlocks[i]._offset = 0;
            _materialBlocks[i]._duration = _duration / _materialBlocks.Length;
            _materialBlocks[i]._respectMeshSize = true;
            if (i < _materialBlocks.Length - 1)
            {
                _materialBlocks[i]._cb = _materialBlocks[i + 1].Animate;
            }
        }
    }

    public void AnimateAllAtOnce()
    {
        for (int i = 0; i < _materialBlocks.Length; i++)
        {
            _materialBlocks[i]._offset = 0;
            _materialBlocks[i]._cb = null;
            _materialBlocks[i]._duration = _duration;
            _materialBlocks[i]._respectMeshSize = false;

            _materialBlocks[i].Animate();
            
        }
    }

    public void Animate()
    {
        GetChildBlocks();
        _materialBlocks[0].Animate();
    }


}
