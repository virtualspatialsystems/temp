
using UnityEngine;
[CreateAssetMenu(fileName = "ZuschauerInfo", menuName = "ScriptableObjects/ZuschauerInfo", order = 1)]
public class ZuschauerInformation : ScriptableObject
{
    [SerializeField]
    private Texture idleFrontTexture;
    [SerializeField]
    private Texture idleBackTexture;

    /*
    Holdes two Textures, front and back, for one visitor in the main hall, such that these defined visitors can be randomly spawned on the seats. 
    */

    public Texture IdleFrontTexture
    {
        get
        {
            return idleFrontTexture;
        }
    }

    public Texture IdleBackTexture
    {
        get
        {
            return idleBackTexture;
        }
    }

   
}