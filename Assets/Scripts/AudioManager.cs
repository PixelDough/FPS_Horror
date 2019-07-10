using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private static AudioMixerGroup mixerGroup;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(this.gameObject);
        }

        mixerGroup = GetComponent<AudioSource>().outputAudioMixerGroup;
    }

    private static GameObject MakeSoundObject()
    {
        GameObject obj = new GameObject("Sound");
        obj.AddComponent<AudioSource>();
        obj.GetComponent<AudioSource>().outputAudioMixerGroup = mixerGroup;

        return obj;
    }

    public static AudioSource PlaySound(AudioClip audioClip)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.Play();
        Destroy(source.gameObject, audioClip.length);
        return source;
    }

    public static AudioSource PlaySound(AudioClip audioClip, float delayInSeconds)
    {
        AudioSource source = MakeSoundObject().GetComponent<AudioSource>();
        source.clip = audioClip;
        source.PlayDelayed(delayInSeconds);
        Destroy(source.gameObject, delayInSeconds + audioClip.length);
        return source;
    }

    public static void PlaySoundRandom(List<AudioClip> audioClips)
    {
        Random.InitState(Random.Range(0, 512));
        AudioSource sound = PlaySound(audioClips[Random.Range(0, audioClips.Count)]);
        sound.pitch = sound.pitch + (Random.Range(-0.25f, 0.5f));
    }
}
