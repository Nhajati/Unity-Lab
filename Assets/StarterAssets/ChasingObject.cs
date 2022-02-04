using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingObject : MonoBehaviour
{
    Vector3 direction;
    CharacterController myCharacter;
    // Start is called before the first frame update
    void Start()
    {
        var character = FindObjectsOfType<CharacterController>();
        myCharacter = character[0];
        var pos = character[0].transform.position;
        direction = pos - transform.position;
        // transform.forward = direction;
    }

    // Update is called once per frame
    void Update()
    {
        direction = myCharacter.transform.position - transform.position;
        transform.Translate(direction * 0.005f);
    }
}
