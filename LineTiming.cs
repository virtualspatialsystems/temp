using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LineTiming : MonoBehaviour
{

    private ChangeOffsetMaterialBlock[] _changeMaterialAlpha;
    public float time = 1;

    [Range(0,1)]
    private float _offset = 1; 
    // Start is called before the first frame update
    void OnEnable()
    {
        _changeMaterialAlpha = GetComponentsInChildren<ChangeOffsetMaterialBlock>();
        
    }

    public void ChangeProperty(float _propertyValue)
    {
        for (int i = 0; i < _changeMaterialAlpha.Length; i++)
        {
            _changeMaterialAlpha[i]._offset = _propertyValue;
        }
    }

}
