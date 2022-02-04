using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomRotation : MonoBehaviour
{
    float theta;
    float nextActionTime = 0.0f;
    float period = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextActionTime)
        {
            // Every one second, the rotation will be performed.
            nextActionTime += period;

            theta = Random.Range(-360.0f, 360.0f);

            for(int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.transform.Rotate(0, theta, 0, Space.Self);
            }
           
        }
        
        
    }
}
