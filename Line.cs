using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[ExecuteAlways, RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    private List<Transform> linePoints = new List<Transform>();
    private LineRenderer _lineRenderer;
    public bool closeCircle = false;
    public bool UseAlphaForScale = false;
    public bool mustUpdateLineWidth = false;

    [MinMax(0, 1, ShowEditRange = false)]
    public Vector2 VariousLineWidth = new Vector2(0, 1);

    public bool shouldUpdate = false;
    public Vector3[] visibleLinePointArray;
    public List<Vector3> _smooth_linePoints = new List<Vector3>();
    private Vector3 _lastPoint = Vector3.zero;
    private Vector3 _lastPosition = Vector3.zero;

    [Range(0f, 2f)]
    public float _lineWidth = 1f;
    [Range(0f, 1f)]
    public float _lineAlpha = 1f;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    float GetLineLength()
    {
        float completeDistance = 0;
        for(int i=0;i<linePoints.Count - 1; i++)
        {
            if(i +1 > linePoints.Count - 1) { continue; }

            completeDistance += Vector3.Distance(linePoints[i].position, linePoints[i + 1].position);

        }

        return completeDistance;
    }


    // Start is called before the first frame update
    List<Vector3> UpdatePoints()
    {
        linePoints.Clear();
        foreach (Transform child in transform)
        {
            linePoints.Add(child.transform);
        }

        if (closeCircle)
        {
            linePoints.Add(transform.GetChild(0).transform);
        }


        Vector3[] _positions = new Vector3[linePoints.Count];
        float[] _scales = new float[linePoints.Count];
        for (int i = 0; i < linePoints.Count; i++)
        {
            _positions[i] = linePoints[i].position;
            _scales[i] = linePoints[i].localScale.x;
        }


        Tuple<Vector3[], float[]> d = LerpStep(LerpStep(new Tuple<Vector3[], float[]>(_positions, _scales)));


        Vector3[] _lerpPos = d.Item1;
        float[] _lerpScale = d.Item2;
        _smooth_linePoints = _lerpPos.ToList();

        int completeLineWidthSegments = (int)GetLineLength() * 10;

        completeLineWidthSegments = completeLineWidthSegments < 20 ? 20 : completeLineWidthSegments;
        completeLineWidthSegments = completeLineWidthSegments > 200 ? 200 : completeLineWidthSegments;

        if (mustUpdateLineWidth) { 
        AnimationCurve curve = new AnimationCurve();

        for(int i = 0; i <= completeLineWidthSegments; i++)
        {
            if(i==0 || i == completeLineWidthSegments) { curve.AddKey(i / completeLineWidthSegments, 0); continue; }
            curve.AddKey((1f / completeLineWidthSegments) * i,  Random.Range(VariousLineWidth.x, VariousLineWidth.y));
        }

        /*
        int maxScalePoints = _lerpScale.Length;
        int realScalePointsCount = (int)((float)maxScalePoints * _lineAlpha);

        var curveLength = UseAlphaForScale ? realScalePointsCount : _lerpScale.Length;

        for (int i = 0; i < curveLength; i++)
        {
            curve.AddKey((1f / curveLength) * i, _lerpScale[i]);
        }*/

        _lineRenderer.widthCurve = curve;
        _lineRenderer.widthMultiplier = _lineWidth;
        }

        float realAlpha = (float)_smooth_linePoints.Count * _lineAlpha;
        int visiblePointCount = (int)(realAlpha);

        float restAlpha = realAlpha - Mathf.Floor(realAlpha);// realPointsCount;

        _lineRenderer.positionCount = visiblePointCount;
        IEnumerable<Vector3> visiblePoints = _smooth_linePoints.Take(visiblePointCount);
        visibleLinePointArray = visiblePoints.ToArray();
        _lineRenderer.SetPositions(visibleLinePointArray);

        return _smooth_linePoints;
    }


    Tuple<Vector3[], float[]> LerpStep(Tuple<Vector3[], float[]> _d)
    {
        List<Vector3> _lerpPos = new List<Vector3>();
        List<float> _lerpScale = new List<float>();
        //add first point
        _lerpPos.Add(_d.Item1[0]);
        _lerpScale.Add(_d.Item2[0]);

        //Calculate lerp position
        for (var i = 1; i < _d.Item1.Length; i++)
        {
            var A = _d.Item1[i - 1];
            var B = _d.Item1[i];

            _lerpPos.Add(Vector3.Lerp(A, B, .15f));
            _lerpPos.Add(Vector3.Lerp(A, B, .25f));
            _lerpPos.Add(Vector3.Lerp(A, B, .5f));
            _lerpPos.Add(Vector3.Lerp(A, B, .75f));
            _lerpPos.Add(Vector3.Lerp(A, B, .85f));

        }

        //Calculate lerp scale
        for (var i = 1; i < _d.Item2.Length; i++)
        {
            var _scaleA = _d.Item2[i - 1];
            var _scaleB = _d.Item2[i];

            _lerpScale.Add(Mathf.Lerp(_scaleA, _scaleB, 0.25f));
            _lerpScale.Add(Mathf.Lerp(_scaleA, _scaleB, 0.75f));

        }

        //add last point
        _lerpPos.Add(_d.Item1[_d.Item1.Length - 1]);

        

        _lerpScale.Add(_d.Item2[_d.Item2.Length - 1]);

        return new Tuple<Vector3[], float[]>(_lerpPos.ToArray(), _lerpScale.ToArray());
    }

    public Vector3[] GetAllLinePoints()
    {

        Vector3[] pos = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(pos);

        return pos;
    }

    public Vector3 GetLastPoint()
    {
        return _lastPosition;
    }

    public Vector3 GetPositionAtProgress(float _progress)
    {
        List<Vector3> _linePoints = UpdatePoints();

        float _lineLength = 0;
        for(int i=0;i<_linePoints.Count - 1; i++)
        {
            _lineLength += Vector3.Distance(_linePoints[i], _linePoints[i + 1]);
        }


        float targetDistance = _lineLength * _progress;
        float _currentDistOnLine = 0;
        int _currentLineIndex = 0;
        float _lastDistance = 0;
        while(_currentDistOnLine < _lineLength * _progress)
        {
            int nextIndex = _currentLineIndex == _linePoints.Count - 1 ? _currentLineIndex : _currentLineIndex + 1;
            _lastDistance =   Vector3.Distance(_linePoints[_currentLineIndex], _linePoints[nextIndex]);
            _currentDistOnLine += _lastDistance;
            _currentLineIndex++;
        }
        
        if(_currentDistOnLine > targetDistance)
        {
            _currentDistOnLine -= _lastDistance;
            _currentLineIndex -= 1;
        }
        
        if( _currentDistOnLine == targetDistance) { return _linePoints[_currentLineIndex]; }

        float nextDist = Vector3.Distance(_linePoints[_currentLineIndex], _linePoints[_currentLineIndex + 1]);
        float t = ((targetDistance - _currentDistOnLine) / nextDist);
        Vector3 targetPosition = Vector3.Lerp(_linePoints[_currentLineIndex], _linePoints[_currentLineIndex + 1], t);

        return targetPosition;
    }

    public Vector3 GetProgress(float _progress)
    {
        UpdatePoints();

        return _lastPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldUpdate) { 
            UpdatePoints();
        }
    }
}
