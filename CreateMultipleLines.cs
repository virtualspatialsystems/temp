using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CreateMultipleLines : MonoBehaviour
{
    GeometryFromLine[] _geoLines;

    private void OnEnable()
    {
        _geoLines = GetComponentsInChildren<GeometryFromLine>();
    }

    public void Convert()
    {
        GameObject _converted = new GameObject("converted-" + name);
        for(int i = 0; i < _geoLines.Length; i++)
        {
            GameObject _gO = _geoLines[i].ConvertToMesh();
            _gO.transform.SetParent(_converted.transform);
        }
    }
}
