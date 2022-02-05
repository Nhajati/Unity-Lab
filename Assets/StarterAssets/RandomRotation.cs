using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomRotation : MonoBehaviour
{

    static float smoothe = 1.0f;
    Quaternion target;

    void Start(){
        target = Random.rotation;
    }
    void Update() {

        if ((transform.GetChild(0).eulerAngles - Quaternion.Euler(0, target.eulerAngles.y, 0).eulerAngles).magnitude < 0.1f) {
            target = Random.rotation;
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            child.transform.rotation = Quaternion.Slerp(child.transform.rotation, Quaternion.Euler(0, target.eulerAngles.y, 0), Time.deltaTime * smoothe);
        } 
        

    }

}

