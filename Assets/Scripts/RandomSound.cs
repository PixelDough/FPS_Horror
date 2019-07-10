using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{

    public List<AudioClip> list;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        audioSource.PlayOneShot(list[Random.Range(0, list.Count)]);
    }
}
