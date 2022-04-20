using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToCharacter : MonoBehaviour
{
    public Transform CharacterToFollow;
    public float speed = 1;
    public float maxDistance = 5;
    public float heightDistance = 0.6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, CharacterToFollow.position.y - heightDistance, transform.position.z);

        if (Vector3.Distance(CharacterToFollow.position, transform.position) > maxDistance) {

            Vector3 direction = transform.position - CharacterToFollow.position;
            Vector3 targetPosition = CharacterToFollow.position + direction.normalized * maxDistance;
            gameObject.transform.position = Vector3.Lerp(targetPosition, transform.position, Time.deltaTime*speed);
            
        }
    }
}
