using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class AlternativeLine : MonoBehaviour
{
    public bool mustUpdate = false;
    public List<GameObject> POIs = new List<GameObject>();
    public float width = 0.1f;
    private List<LineRenderer> _lines = new List<LineRenderer>();
    private GameObject LineHolder;

    public Material _defaultMat;

    private void Awake()
    {
        //Shader _shader = Shader.Find("DTHG/GradientLineMaterial");
        _defaultMat = _defaultMat == null ? new Material(Shader.Find("Unlit/Color")) : _defaultMat;

        GameObject _lineHolder = GameObject.Find("LineHolder");

        if(_lineHolder != null)
        {
            DestroyImmediate(_lineHolder);
        }
        CreateLine();
    }

    void CreateHolder()
    {
        LineHolder = new GameObject("LineHolder");
    }
    private void OnDisable()
    {
        if (LineHolder != null)
        {
            DestroyImmediate(LineHolder);
        }
    }

    private void OnDestroy()
    {
        if (LineHolder != null)
        {
            DestroyImmediate(LineHolder);
        }
    }

    void CreateLine()
    {
        if(LineHolder != null)
        {
            DestroyImmediate(LineHolder);
        }

        CreateHolder();
        //init line List
        _lines = new List<LineRenderer>();
        POIs = new List<GameObject>();

        //Get All children first hierarchyLevel
        Transform _lastChild;
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        // Create a LineSegment per ChildObject
        int childIndex = -1;
        foreach(Transform child in children) {

            

            childIndex++;
            POIs.Add(child.gameObject);

            if(childIndex == 0) { continue; }
            
            _lines.Add(CreateLineSegment(children[childIndex - 1].gameObject, child.position));
        }
    }

    LineRenderer CreateLineSegment(GameObject startObject, Vector3 endPos)
    {
        GameObject _empty = new GameObject("LineSegment");
                   _empty.transform.parent = LineHolder.transform;
        LineRenderer _line = _empty.AddComponent<LineRenderer>();
        _line.widthMultiplier = width;
        
        float alpha = 1.0f;
        _line.material = _defaultMat;


        List<Vector3> _linePoints = new List<Vector3>();
        //add startpoint to line
        _linePoints.Add(startObject.transform.position);
        //add child points to line
        foreach (Transform child in startObject.transform)
        {
            _linePoints.Add(child.transform.position);
        }
        //add endpoint to line
        _linePoints.Add(endPos);


        //Lerp N times the original Linepoints
        List<Vector3> _lerpedPoints = Lerper(Lerper(_linePoints));
        
        _line.positionCount = _lerpedPoints.Count;
        _line.SetPositions(_lerpedPoints.ToArray());

        _line.startColor = Color.white;
        _line.endColor = Color.white;

        return _line;
    }

    List<Vector3> Lerper(List<Vector3> _linePoints)
    {
        int index = -1;
        List<Vector3> lerpLinePoints = new List<Vector3>();
        lerpLinePoints.Add(_linePoints[0]);
        _linePoints.ForEach((Vector3 _vec) =>
        {
            index++;
            if (index == 0) { return; }

            lerpLinePoints.Add(Vector3.Lerp(_linePoints[index - 1], _vec, .25f));
            lerpLinePoints.Add(Vector3.Lerp(_linePoints[index - 1], _vec, .5f));
            lerpLinePoints.Add(Vector3.Lerp(_linePoints[index - 1], _vec, .75f));
        });

        lerpLinePoints.Add(_linePoints[_linePoints.Count - 1]);
        return lerpLinePoints;
    }


    public float GetLineLength(int lineIndex)
    {
        //Lerp N times the original Linepoints
        Vector3[] _lerpedPositions = new Vector3[_lines[lineIndex].positionCount];
        _lines[lineIndex].GetPositions(_lerpedPositions);
       
        float completeLineLength = 0;
        for (int i = 1; i < _lerpedPositions.Length; i++)
        {
            completeLineLength += Vector3.Distance(_lerpedPositions[i - 1], _lerpedPositions[i]);
        }

        //Debug.Log("complete Length of Line" + lineIndex + ": " + completeLineLength);
        return completeLineLength;
    }

    public Vector3 GetPOIStartPosition(int POIIndex)
    {
        //wenn nächster punkt kein neuer POI ist sondern das ende des Paths
        //return ende von letzem Linerenderer
        if( POIIndex > _lines.Count - 1) {

            Debug.Log(POIIndex + "last");
            return POIs[POIs.Count - 1].transform.position;
        }
        return _lines[POIIndex].GetPosition(0);
    }
    public List<LineRenderer> GetAllPOIs()
    {
        return _lines;
    }

    public Vector3 GetPositionAtProgressFromLine(int _lineIndex, float _progress)
    {

        Vector3[] _linePointsArray = new Vector3[_lines[_lineIndex].positionCount];
        List<Vector3> _linePoints = new List<Vector3>();
            
        _lines[_lineIndex].GetPositions(_linePointsArray);


        for (int i = 0; i < _linePointsArray.Length; i++)
        {
            _linePoints.Add(_linePointsArray[i]);
        }


        float _lineLength = 0;
        for (int i = 0; i < _linePoints.Count - 1; i++)
        {
            _lineLength += Vector3.Distance(_linePoints[i], _linePoints[i + 1]);
        }


        float targetDistance = _lineLength * _progress;
        float _currentDistOnLine = 0;
        int _currentLineIndex = 0;
        float _lastDistance = 0;
        while (_currentDistOnLine < _lineLength * _progress)
        {
            int nextIndex = _currentLineIndex == _linePoints.Count - 1 ? _currentLineIndex : _currentLineIndex + 1;
            _lastDistance = Vector3.Distance(_linePoints[_currentLineIndex], _linePoints[nextIndex]);
            _currentDistOnLine += _lastDistance;
            _currentLineIndex++;
        }

        if (_currentDistOnLine > targetDistance)
        {
            _currentDistOnLine -= _lastDistance;
            _currentLineIndex -= 1;
        }

        if (_currentDistOnLine == targetDistance) { return _linePoints[_currentLineIndex]; }

        float nextDist = Vector3.Distance(_linePoints[_currentLineIndex], _linePoints[_currentLineIndex + 1]);
        float t = ((targetDistance - _currentDistOnLine) / nextDist);
        Vector3 targetPosition = Vector3.Lerp(_linePoints[_currentLineIndex], _linePoints[_currentLineIndex + 1], t);

        return targetPosition;
        
    }

    void Update()
    {
        if (mustUpdate && !Application.isPlaying)
        {
            CreateLine();
        }
    }

}
