using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private GameObject MakeSoundObject()
    {
        GameObject obj = new GameObject("Sound");
        obj.AddComponent<AudioSource>();

        return obj;
    }

    public void PlaySound(AudioClip audioClip)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.Play();
        Destroy(source.gameObject, audioClip.length);
    }

    public void PlaySound(AudioClip audioClip, float delayInSeconds)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.PlayDelayed(delayInSeconds);
        Destroy(source.gameObject, delayInSeconds + audioClip.length);
    }
}
