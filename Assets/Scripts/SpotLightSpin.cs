using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightSpin : MonoBehaviour
{
    public GameObject lightRotator;
    float startRange;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //light = GetComponent<Light>();
        //startRange = lightRotator.range;
    }

    // Update is called once per frame
    void Update()
    {
        lightRotator.transform.Rotate(0, 0, 3);
    }
}
