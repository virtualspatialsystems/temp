using UnityEngine;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEditorInternal;

[CustomTimelineEditor ( typeof ( TranslationClip ) )]
public class TranslationClipEditor : ClipEditor
{
    public override ClipDrawOptions GetClipOptions ( TimelineClip clip )
    {
        var clipOptions = base.GetClipOptions ( clip );
        var characterProfile = ( ( TranslationClip ) clip.asset ).translations;
        clipOptions.highlightColor = Color.cyan;
        return clipOptions;
    }
}

[CustomEditor(typeof(TranslationClip))]
public class TranslationEditor : Editor{

    private ReorderableList reorderableList;
    
    private TranslationClip clip
    {
        get
        {
            return target as TranslationClip;
        }
    }

    private void DrawHeader(Rect rect)
    {
        GUI.Label(rect, "Translation ( # legt einen Anzeigeabschnitt innerhalb des Textes fest)");
    }
    private void DrawElement(Rect rect, int index, bool active, bool focused)
    {
        EditorGUI.BeginChangeCheck();
        
        float itemHeight = reorderableList.elementHeight;
        float singleLineHeight = EditorGUIUtility.singleLineHeight;
        TranslationObject item = clip.translations[index];


        EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, singleLineHeight), "Sprache");
        
        item.language = (Language)EditorGUI.EnumPopup(new Rect(rect.x, rect.y + singleLineHeight * 1, 70 , singleLineHeight) , item.language);
        
        
        EditorGUI.LabelField(new Rect(rect.x + 60, rect.y, rect.width, singleLineHeight), "Übersetzung");
        EditorStyles.textArea.wordWrap = true;

        item.text = EditorGUI.TextArea(new Rect(rect.x + 60, rect.y + singleLineHeight * 1, rect.width - 80 , singleLineHeight * 3) , item.text);



        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }


        while(clip.translations.Count < 2){
            AddItem( reorderableList );
        }

        clip.translations[0].language = (clip.translations[0].language != Language.DE) ? Language.DE : clip.translations[0].language;
        clip.translations[1].language = (clip.translations[1].language != Language.EN) ? Language.EN : clip.translations[1].language;
        
       
    }

    
    private void AddItem(ReorderableList list)
    {
        clip.translations.Add(new TranslationObject());

        EditorUtility.SetDirty(target);
    }

    private void RemoveItem(ReorderableList list)
    {
        clip.translations.RemoveAt(list.index);

        EditorUtility.SetDirty(target);
    }


    private void OnEnable()
    {
        reorderableList = new ReorderableList(clip.translations , typeof(TranslationClip), true, true, true, true);

        // Add listeners to draw events
        reorderableList.drawHeaderCallback += DrawHeader;
        reorderableList.drawElementCallback += DrawElement;

        reorderableList.onAddCallback += AddItem;
        reorderableList.onRemoveCallback += RemoveItem;

        reorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 4.5f;
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        
        clip.TranslationName = EditorGUI.TextField(new Rect( 0, EditorGUIUtility.singleLineHeight * 1, 200 , EditorGUIUtility.singleLineHeight * 1) , clip.TranslationName);

        // Actually draw the list in the inspector
        reorderableList.DoLayoutList();
       
    }
}