//Attach this script to the Game Object you're using as your First Person Controller. Only include one instance of this sciprt as a component in each scene of the game.

using UnityEngine;

public class FMODStudioFirstPersonFootsteps : MonoBehaviour
{
    
    [Header("FMOD Settings")]
    [SerializeField] [FMODUnity.EventRef] private string FootstepsEventPath;    
   
    [SerializeField] private string MaterialParameterName;                      
    //[SerializeField] private string SpeedParameterName;                        
    
    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;                         
    [SerializeField] private float RayDistance = 1.2f;                          
    
    
    public string[] MaterialTypes;                                              
    [HideInInspector] public int DefulatMaterialValue; 

    [HideInInspector] public int CharacterValue;                         


   
    private float StepRandom;                                                   
    private Vector3 PrevPos;                                                    
    private float DistanceTravelled;                                            
   
    private RaycastHit hit;                                                     
    private int F_MaterialValue;                                                
    
    private float TimeTakenSinceStep;                                           


    void Start() 
    {
        StepRandom = Random.Range(0f, 0.2f);        
        PrevPos = transform.position;               
    }


    void Update() 
    {

        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue);       


        TimeTakenSinceStep += Time.deltaTime;                                
        DistanceTravelled += (transform.position - PrevPos).magnitude;       
        if (DistanceTravelled >= StepDistance + StepRandom)                  
        {
           // MaterialCheck();                                                
                                                       
            PlayFootstep();                                                 
            StepRandom = Random.Range(0f, 0.07f);                             
            DistanceTravelled = 0f;                                          
        }
        PrevPos = transform.position;                                        

    }


    void MaterialCheck() 
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))                                 
        {
            if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())                                    
            {
                F_MaterialValue = hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>().MaterialValue;    
            }
            else                                                                                                     
                F_MaterialValue = DefulatMaterialValue;                                                              
        }
        else                                                                                                         
            F_MaterialValue = DefulatMaterialValue;                                                                  
    }


    private void OnTriggerStay(Collider other)
    {
    	if (other.tag == "AudioMaterial")
        	{
            	F_MaterialValue = other.gameObject.GetComponent<FMODStudioMaterialSetter>().MaterialValue;
        	}
    }

    private void OnTriggerExit(Collider other)
    {
        F_MaterialValue = DefulatMaterialValue;
    }

    void PlayFootstep() 
    {
                                                                                        
        {
            FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath);        
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>());     
            Footstep.setParameterByName(MaterialParameterName, F_MaterialValue);
            Footstep.setParameterByName("Character", CharacterValue);                                  
            Footstep.start();                                                                                        
        }
    }


}

