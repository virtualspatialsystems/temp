using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[ExecuteAlways, RequireComponent(typeof(LineRenderer))]
public class LineFromMesh : MonoBehaviour
{
    private List<Transform> linePoints = new List<Transform>();
    private LineRenderer _lineRenderer;
    private MeshFilter _meshFilter;
    public bool UseAlphaForScale = false;
    [Range(0f, 1f)]
    public float _lineWidth = 1f;
    [Range(0f, 1f)]
    public float _lineAlpha = 1f;

    private void OnEnable()
    {

        Init();
       
    }


    void Init()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _meshFilter = GetComponent<MeshFilter>();
    }


    // Start is called before the first frame update
    void UpdatePoints()
    {
        Init();
        linePoints.Clear();

        // Vector3[] d = LerpStep(LerpStep(_meshFilter.sharedMesh.vertices));
        Debug.Log(_meshFilter.sharedMesh.vertices.Length);

        Vector3[] _lerpPos = _meshFilter.sharedMesh.vertices;


        Debug.Log(_lerpPos.Length);

        int maxPoints = _lerpPos.Length;
        int realPointsCount = (int)((float)maxPoints * _lineAlpha);

        _lineRenderer.positionCount = realPointsCount;
        IEnumerable<Vector3> realPoints = _lerpPos.Take(realPointsCount);
        _lineRenderer.SetPositions(realPoints.ToArray());
    }

    Vector3[] LerpStep(Vector3[] _d)
    {
        List<Vector3> _lerpPos = new List<Vector3>();
        List<float> _lerpScale = new List<float>();
        //add first point
        _lerpPos.Add(_d[0]);

        //Calculate lerp position
        for (var i = 1; i < _d.Length; i++)
        {
            var A = _d[i - 1];
            var B = _d[i];

            _lerpPos.Add(Vector3.Lerp(A, B, .25f));
            _lerpPos.Add(Vector3.Lerp(A, B, .5f));
            _lerpPos.Add(Vector3.Lerp(A, B, .75f));

        }


        //add last point
        _lerpPos.Add(_d[_d.Length - 1]);

        return _lerpPos.ToArray();
    }

    private void Update()
    {
        UpdatePoints();
    }
}
