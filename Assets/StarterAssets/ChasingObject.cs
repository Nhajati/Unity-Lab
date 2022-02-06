using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IGS520b.starter.SampleGame
{    
public class ChasingObject : MonoBehaviour
{
    Vector3 direction;
    Vector3 characterSize;
    Vector3 characterMidPoint;
    CharacterController myCharacter;
    bool robotCrashed;
    float minDistance;
    float nowDistance;

    void Start()
    {
        robotCrashed = false;
        
        // Find the character
        var character = FindObjectsOfType<CharacterController>();
        myCharacter = character[0];

        // Get the height of the character, so that we could command the robot to follow its middle point
        characterSize = myCharacter.GetComponent<Collider>().bounds.size; 
        characterMidPoint = new Vector3(myCharacter.transform.position.x, characterSize.y / 2, myCharacter.transform.position.z);

    }

    void Update()
    {
        GameManager[] gameManager = FindObjectsOfType<GameManager>();
        // Debug.Log(HasCrashedIntoCharacter());

        if(gameManager[0].GetStartChasing()){

            if(HasCrashedIntoCharacter()){
                CrashedIntoCharacter();
            } else {
            // Update the midpoint position of the character
            characterMidPoint.x = myCharacter.transform.position.x;
            characterMidPoint.y = characterSize.y / 2;
            characterMidPoint.z = myCharacter.transform.position.z;
            direction = characterMidPoint - transform.position;
        
            // Make the enemy object face the character
            transform.forward = direction;
            Debug.DrawRay(transform.position, transform.forward, Color.red); // Need to turn on Gizmos to make it show!

            // Make the enemy object follow the character 
            transform.Translate(direction * 0.005f);

            }
        }
       
    }

    // To check if the robot crashed into the character
    bool HasCrashedIntoCharacter() 
    {   
        var theCharacter = FindObjectsOfType<CharacterController>();
        // MAKE SURE THEY HAVE ONLY ONE COLLIDER!
        Collider characterCollider = theCharacter[0].GetComponent<Collider>(); 
        SphereCollider robotCollider = GetComponent<SphereCollider>();

        nowDistance = Mathf.Sqrt(Mathf.Pow(transform.position.x - theCharacter[0].transform.position.x, 2) + Mathf.Pow(transform.position.z - theCharacter[0].transform.position.z, 2));
        // minDistance = Mathf.Abs(robotCollider.radius - Mathf.Sqrt(Mathf.Pow(characterCollider.bounds.size.x, 2) + Mathf.Pow(characterCollider.bounds.size.z, 2)));
        minDistance = 0.9f;
        Debug.Log(nowDistance);
        Debug.Log(minDistance);
        Debug.Log(robotCrashed);
        
        if(nowDistance <= minDistance){
            robotCrashed = true;
        } else {
            robotCrashed = false;
        }
        return robotCrashed;
    }

    // What to do when the robot crashes into the character
    void CrashedIntoCharacter()
    {
        // health -= 1;
    }
}
}