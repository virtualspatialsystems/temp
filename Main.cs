using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using Valve.VR;

public class Main : MonoBehaviour
{

    public GameObject _player;
    //public GameObject _map;
    public Color background;
    public GameObject _mapAnchor;

    private void Awake()
    {

        //Debug.Log(Camera.main.name);
        //_player = Camera.main.transform.root.gameObject;
        //_player.GetComponent<StickToCharacter>().enabled = true;
        //_player.GetComponent<Locomotion_SupermanStyle>().enabled = true;
        //_player.GetComponent<PointerController>().SetPointerActive(false);
        //_player.GetComponent<PointerController>().ActiveLeftTeleportPlane(false, false);
        //_player.GetComponent<PointerController>().ActiveSupermanPlane(true);
        //Camera.main.gameObject.GetComponent<Camera>().backgroundColor = background;

        

       // _map.transform.parent = _player.transform;

        /*foreach (PlayableDirector p in GameObject.FindObjectsOfType(typeof(PlayableDirector))){
            p.time = 0;
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log("Start One Show: "+ StaticClass.CrossSceneInformation);
        if (StaticClass.CrossSceneInformation == StaticClass.FRITZI)
        {
            //Debug.Log("Fritzi");
            _player.GetComponent<StickToCharacter>().CharacterToFollow = _fritzi;
        }
        else if (StaticClass.CrossSceneInformation == StaticClass.VISITOR)
        {
            //Debug.Log("Visitor");
            _player.GetComponent<StickToCharacter>().CharacterToFollow = _visitor;
        }

        ExitButton.OnExitOneShowClicked += BackToOneShowMenu;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

}
