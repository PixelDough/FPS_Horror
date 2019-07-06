using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    
    public float xRange;
    public float yRange;
    public float zRange;

    void Awake()
    {
        Quaternion startRotation = transform.rotation;

        transform.eulerAngles = (new Vector3(
            startRotation.eulerAngles.x + Random.Range(-xRange, xRange), 
            startRotation.eulerAngles.y + Random.Range(-yRange, yRange), 
            startRotation.eulerAngles.z + Random.Range(-zRange, zRange)
            ));
    }
    
}
