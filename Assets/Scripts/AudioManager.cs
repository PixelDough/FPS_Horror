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

    public AudioSource PlaySound(AudioClip audioClip)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.Play();
        Destroy(source.gameObject, audioClip.length);
        return source;
    }

    public AudioSource PlaySound(AudioClip audioClip, float delayInSeconds)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.PlayDelayed(delayInSeconds);
        Destroy(source.gameObject, delayInSeconds + audioClip.length);
        return source;
    }

    public void PlaySoundRandom(List<AudioClip> audioClips)
    {
        Random.InitState(Random.Range(0, 512));
        AudioSource sound = PlaySound(audioClips[Random.Range(0, audioClips.Count)]);
        sound.pitch = sound.pitch + (Random.Range(-0.25f, 0.5f));
    }
}
