using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomScale : MonoBehaviour
{

    public float minScale = 1;
    public float maxScale = 1;

    void Awake()
    {
        Vector3 startScale = transform.localScale;

        float scale = Random.Range(minScale, maxScale);

        transform.localScale *= scale;

        //transform.localScale = (new Vector3(
        //    transform.localScale.x * scale,
        //    transform.localScale.y * scale,
        //    transform.localScale.z * scale
        //    ));
    }
    
}
