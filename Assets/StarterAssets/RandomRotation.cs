using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomRotation : MonoBehaviour
{
    float theta;
    float previousTheta;
    float incrementsOfTheta;
    static float smooth = 0.5f;

    void Start() 
    {
        transform.rotation = new Quaternion(20, 20, 20, -1);
        theta = Random.Range(-360.0f, 360.0f);
        previousTheta = 0;
    }


    void Update()
    {
        Quaternion target = Quaternion.Euler(0, theta, 0);

        if ((transform.eulerAngles - target.eulerAngles).magnitude < 0.001f) 
        {
            previousTheta = theta;
            theta = Random.Range(-360.0f, 360.0f);
        }

        float timeNow = Time.deltaTime;

        for(int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            Debug.Log(transform.rotation);
            // THE PROBLEM IS THAT TRANSFORM.ROTATION IS ZERO IN THE FIRST MOMENT.
            child.transform.rotation = Quaternion.Slerp(transform.rotation, target, timeNow * smooth);
        } 

    }


}

