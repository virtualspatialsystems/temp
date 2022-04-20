using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public GameObject _interactionPointer;
    public GameObject _selectPlane;
    public GameObject _menuPlane;
    public GameObject _supermanPlane;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPointerActive(bool active) {
        _interactionPointer.SetActive(active);
    }

    public void ActiveLeftTeleportPlane(bool selectActive, bool menuActive) {
        _selectPlane.SetActive(selectActive);
        _menuPlane.SetActive(menuActive);
    }

    public void ActiveSupermanPlane(bool active) {
        _supermanPlane.SetActive(active);
    }

}
