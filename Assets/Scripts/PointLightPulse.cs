using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightPulse : MonoBehaviour
{
    Light light;
    float startRange;
    float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        startRange = light.range;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time++;
        light.range = startRange + Mathf.Sin(time / 50f) * 3f;
    }
}
