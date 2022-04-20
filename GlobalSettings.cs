using UnityEngine;

[CreateAssetMenu(fileName = "GlobalValues", menuName = "ScriptableObjects/GlobalSettings", order = 1)]
public class GlobalSettings : ScriptableObject
{
    [SerializeField]
    private Color darkToonColor;

    [SerializeField]
    private Color midToonColor;

    [SerializeField]
    private Color lightToonColor;

    [SerializeField]
    private Color darkToonColorOutside;

    [SerializeField]
    private Color midToonColorOutside;

    [SerializeField]
    private Color lightToonColorOutside;

    [SerializeField]
    private Texture2D paperBack;

    [SerializeField]
    private Color backgroundCharacter;


    public Color DarkToonColor
    {
        get
        {
            return darkToonColor;
        }
    }

    public Color MidToonColor
    {
        get
        {
            return midToonColor;
        }
    }

    public Color LightToonColor
    {
        get
        {
            return lightToonColor;
        }
    }

    public Color DarkToonColorOutside
    {
        get
        {
            return darkToonColorOutside;
        }
    }

    public Color MidToonColorOutside
    {
        get
        {
            return midToonColorOutside;
        }
    }

    public Color LightToonColorOutside
    {
        get
        {
            return lightToonColorOutside;
        }
    }

    public Texture PaperBack
    { 
        get
        {
            return paperBack;
        }
    }

    public Color BackgroundCharacter
    {
        get
        {
            return backgroundCharacter;
        }
    }
}