using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MultiLineChanger : MonoBehaviour
{
    [Range(0f,1f)]
    public float lineAlpha = 1f;
    private float _lastlineAlpha = 1f;

    public bool mustUpdate = false;
    private bool _lastShouldUpdate = false;

    [MinMax(0, 5, ShowEditRange = false)]
    public Vector2 VariousLineWidth = new Vector2(0, 1);
    private Vector2 _lastVariousLineWidth = new Vector2(0, 1);

    [Range(0,1)]
    public float LineWidth = 1;
    private float _lastLineWidth = 1;

    Line[] lines;

    private void Awake()
    {
        lines = UpdateLineScripts();
    }
    
    List<Line> GetAllLinesInChildren(List<Line> lineScripts,  GameObject obj )
    {       

        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;

            if(child.GetComponent<Line>() != null)
            {
                lineScripts.Add(child.GetComponent<Line>());
            }

            GetAllLinesInChildren(lineScripts, child.gameObject);
        }

        return lineScripts;
    }


    Line[] UpdateLineScripts()
    {
        List<Line> lineScripts = new List<Line>();

        lineScripts = GetAllLinesInChildren(lineScripts, gameObject);
        

        return lineScripts.ToArray();

    }


    void ChangeLines()
    {
        lines = UpdateLineScripts();

        foreach (Line l in lines)
        {
            if(l == null) { continue; }

            l._lineAlpha = lineAlpha;
        }

        _lastlineAlpha = lineAlpha;
    }


    void ChangeUpdateLines()
    {
        lines = UpdateLineScripts();
        
        foreach (Line l in lines)
        {
            if (l == null) { continue; }

            l.shouldUpdate = mustUpdate;
        }

        _lastShouldUpdate = mustUpdate;
    }


    void ChangeVariousLineWidth()
    {
        lines = UpdateLineScripts();

        foreach (Line l in lines)
        {
            if (l == null) { continue; }

            l.VariousLineWidth = VariousLineWidth;
        }

        _lastVariousLineWidth = VariousLineWidth;
    }

    void ChangeLineWidth()
    {
        lines = UpdateLineScripts();

        foreach (Line l in lines)
        {
            if (l == null) { continue; }

            l._lineWidth = LineWidth;
        }

        _lastLineWidth = LineWidth;
    }



    // Update is called once per frame
    void Update()
    {
        if(_lastlineAlpha != lineAlpha)
        {
            ChangeLines();
        }

        if(_lastShouldUpdate != mustUpdate)
        {
            ChangeUpdateLines();
        }

        if(_lastVariousLineWidth != VariousLineWidth)
        {
            ChangeVariousLineWidth();
        }

        if(_lastLineWidth != LineWidth)
        {
            ChangeLineWidth();
        }

    }
}
