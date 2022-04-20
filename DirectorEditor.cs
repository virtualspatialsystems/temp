using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.Timeline;
using UnityEngine.Playables;


[CustomEditor(typeof(Director))]
public class DirectorEditor : Editor
{
    private ReorderableList reorderableList;
    private float baseHeight = EditorGUIUtility.singleLineHeight * 13f;
    private int baseListCount = 3;
    
    private Director director
    {
        get
        {
            return target as Director;
        }
    }

    private void OnEnable()
    {
        reorderableList = new ReorderableList(director.list, typeof(DirectorItem), true, true, true, true);

        // Add listeners to draw events
        reorderableList.drawHeaderCallback += DrawHeader;
        reorderableList.drawElementCallback += DrawElement;

        reorderableList.onAddCallback += AddItem;
        reorderableList.onRemoveCallback += RemoveItem;

        reorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 15.5f;
    }

    private void OnDisable()
    {
        // Make sure we don't get memory leaks etc.
        reorderableList.drawHeaderCallback -= DrawHeader;
        reorderableList.drawElementCallback -= DrawElement;

        reorderableList.onAddCallback -= AddItem;
        reorderableList.onRemoveCallback -= RemoveItem;
    }

    /// <summary>
    /// Draws the header of the list
    /// </summary>
    /// <param name="rect"></param>
    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Director Liste");
    }

    private float TextLineY(float row)
    {
        return 18 * row;
    }

    /// <summary>
    /// Draws one element of the list (ListItemExample)
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="index"></param>
    /// <param name="active"></param>
    /// <param name="focused"></param>
    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        float _gesamtZeit = 0;
        director.list.ForEach((DirectorItem _item) => { _gesamtZeit += _item.waitDuration + _item.durationToNextTarget; });
        float _ankunftsZeit = 0;
        director.list.GetRange(0, index ).ForEach((DirectorItem _item) => { _ankunftsZeit += _item.waitDuration + _item.durationToNextTarget; });

        float itemHeight = reorderableList.elementHeight;
        float singleLineHeight = EditorGUIUtility.singleLineHeight;

        DirectorItem item = director.list[index];
        DirectorItem _lastItem = index >=1 ? director.list[index - 1] : null;
        DirectorItem nextItem = director.list.Count < index + 1 ? director.list[index + 1] : null; 

        EditorGUI.BeginChangeCheck();

        EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, singleLineHeight), "POI Name", EditorStyles.boldLabel);
        //Name
        item.directorItemName = EditorGUI.TextField(new Rect(rect.x, rect.y + TextLineY(1), rect.width, singleLineHeight), item.directorItemName);

        //Progress
        EditorGUI.LabelField(new Rect(rect.x, rect.y + TextLineY(2), rect.width / 4, singleLineHeight), "Progress", EditorStyles.boldLabel);
        item.Progress = EditorGUI.FloatField(new Rect(rect.x, rect.y + TextLineY(3), rect.width / 4 - 20, singleLineHeight), item.Progress);

        //Wartezeit
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 , rect.y + TextLineY(2), rect.width / 4, singleLineHeight), "Wartezeit auf POI");
        item.waitDuration = EditorGUI.FloatField(new Rect(rect.x + rect.width / 4, rect.y + TextLineY(3), rect.width / 4 - 20, singleLineHeight), item.waitDuration);
        
        //DurationToNextTarget
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 2, rect.y + TextLineY(2), rect.width / 4, singleLineHeight), "Transitionzeit");
        item.durationToNextTarget = EditorGUI.FloatField(new Rect(rect.x + rect.width / 4 * 2, rect.y + TextLineY(3), rect.width / 4 - 20, singleLineHeight), item.durationToNextTarget);
      
        //Time Info
        //Time Info
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(2), rect.width / 4, singleLineHeight), "Zeit Info", EditorStyles.boldLabel);
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(3), rect.width / 4, singleLineHeight), $"Ankunft: {_ankunftsZeit}s/{_gesamtZeit}s" );
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(4), rect.width / 4, singleLineHeight), $"Abfahrt: {_ankunftsZeit + item.waitDuration}s" );
        

    // smooth out Transition Between POI 
        float endPoint = item.movingInterpolation.Evaluate(1);
        float checkPoint = item.movingInterpolation.Evaluate(.9f);

        float steepPoint = checkPoint / endPoint;
        EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(6), rect.width / 4, singleLineHeight), $"steep: {1 - steepPoint}" );

        //Rect buttonRect = new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(7), rect.width / 4, singleLineHeight);

        //if (GUI.Button(buttonRect, "Transition"))
        //{
        //  item.movingInterpolation = director.TransitionToLastPOI(index - 1);
        //  Debug.Log("click");
        //}



    float GetMeterPerSeconds(int _index,float percentageMargin, bool fromEnd  ){

      var start = fromEnd ? 1 : 0;
      var end = fromEnd ? 1 - percentageMargin : percentageMargin;
      

      Vector3 endPos = director.mainCameraLine.GetPositionAtProgressFromLine(_index, start);
      Vector3 checkPos = director.mainCameraLine.GetPositionAtProgressFromLine(_index, end);

      float checkDistance = Vector3.Distance(checkPos, endPos);
      float meterPerSek = 1f / (item.durationToNextTarget * .1f) * checkDistance;

      return meterPerSek;
    }

    if(director.mainCameraLine != null)
    {
      float meterPerSek = GetMeterPerSeconds(index, .1f, true);
      float nextMeterPerSek = (index + 1 < director.list.Count) ? GetMeterPerSeconds(index + 1, .1f, true) : 0;
     

      EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(7), rect.width / 4, singleLineHeight), $"m/s -> last 10%: { meterPerSek }m/s" );
      EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(8), rect.width / 4, singleLineHeight), $"m/s -> next POI first 10% { nextMeterPerSek }m/s" );
      EditorGUI.LabelField(new Rect(rect.x + rect.width / 4 * 3, rect.y + TextLineY(9), rect.width / 4, singleLineHeight), $"difference m/s { meterPerSek - nextMeterPerSek }m/s" );


      
        

    }
    
    
    //Interpolation

    EditorGUI.LabelField(new Rect(rect.x, rect.y + TextLineY(4), 50 , singleLineHeight), "Benutze Interpolation");
        item.useAnimationCurve = EditorGUI.Toggle(new Rect(rect.x, rect.y + TextLineY(5), 50, singleLineHeight), item.useAnimationCurve);

    
        EditorGUI.LabelField(new Rect(rect.x + 50, rect.y + TextLineY(4), (rect.width / 4) -50 , singleLineHeight), "Transition Interpolation");
        item.movingInterpolation = EditorGUI.CurveField(new Rect(rect.x + 50, rect.y + TextLineY(5), rect.width / 4 * 3 - 70, singleLineHeight * 2 ), item.movingInterpolation);
    
    


    //Timelines
    EditorGUI.LabelField(new Rect(rect.x , rect.y + TextLineY(7), rect.width / 4, singleLineHeight), "Timeline für POI", EditorStyles.boldLabel );
        var list = item.timelines;
        int newCount = Mathf.Max(0, EditorGUI.IntField(new Rect(rect.x, rect.y + TextLineY(8), 18, singleLineHeight), list.Count));
        while (newCount < list.Count)
            list.RemoveAt(list.Count - 1);
        while (newCount > list.Count)
            list.Add(null);

        for (int i = 0; i < list.Count; i++)
        {
            list[i] = (PlayableDirector)EditorGUI.ObjectField(new Rect(rect.x + 25, rect.y + TextLineY(8) + i * (singleLineHeight + 5), rect.width/4*3 - 25, singleLineHeight), list[i], typeof(PlayableDirector));
        }


        item.timelines = list;
        
        //Visual Line
        if (index != director.list.Count - 1)
        {
            EditorGUI.LabelField(new Rect(rect.x - 15, rect.y + TextLineY(13), rect.width + 15, singleLineHeight), "", GUI.skin.horizontalSlider);
        }

        //Set index in List for item
        item._indexInGlobalList = index;
        item._arriveAtPosition = _ankunftsZeit;
        item._globalTime = _gesamtZeit;

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }

    }

    private void AddItem(ReorderableList list)
    {
        director.list.Add(new DirectorItem());

        EditorUtility.SetDirty(target);
    }

    private void RemoveItem(ReorderableList list)
    {
        director.list.RemoveAt(list.index);

        EditorUtility.SetDirty(target);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Actually draw the list in the inspector
        reorderableList.DoLayoutList();
       
    }
}
