using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateFrames : MonoBehaviour
{

    public Sprite[] frames;
    public float framesPerSecond = 10f;

    // Update is called once per frame
    void Update()
    {

        float index = (Time.time * framesPerSecond) % frames.Length;
        GetComponent<Image>().sprite = frames[Mathf.FloorToInt(index)];
    }
}
