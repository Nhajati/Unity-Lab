using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingObject : MonoBehaviour
{
    Vector3 direction;
    Vector3 characterSize;
    Vector3 characterMidPoint;
    CharacterController myCharacter;

    void Start()
    {
        // Find the character
        var character = FindObjectsOfType<CharacterController>();
        myCharacter = character[0];

        // Get the height of the character, so that we could command the robot to follow its middle point
        characterSize = myCharacter.GetComponent<Collider>().bounds.size; 
        characterMidPoint = new Vector3(myCharacter.transform.position.x, characterSize.y / 2, myCharacter.transform.position.z);

    }


    void Update()
    {
        // Update the midpoint position of the character
        characterMidPoint.x = myCharacter.transform.position.x;
        characterMidPoint.y = characterSize.y / 2;
        characterMidPoint.z = myCharacter.transform.position.z;
        direction = characterMidPoint - transform.position;
        
        // Make the enemy object face the character
        transform.forward = direction;
        // Debug.DrawRay(transform.position, transform.forward, Color.red); // Need to turn on Gizmos to make it show!

        // Make the enemy object follow the character 
        transform.Translate(direction * 0.005f);
    }
}
