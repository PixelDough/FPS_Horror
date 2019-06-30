using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTape : Interactive
{
    public AudioClip tapeOpenEffectSound;
    public AudioClip tapeCloseEffectSound;
    public AudioClip audioClip;
    public Material TVStatic;

    [TextArea(15, 20)]
    public string transcription;

    UIGameManager uiGame;
    Material startMaterial;

    //AudioSource audioSource;

    public override void Start()
    {
        base.Start();

        uiGame = FindObjectOfType<UIGameManager>();

        startMaterial = GetComponentInChildren<MeshRenderer>().material;

        //audioSource = gameObject.AddComponent<AudioSource>();
    }

    public override void Update()
    {
        GetComponentInChildren<MeshRenderer>().material = startMaterial;
    }

    public override void LookAt()
    {
        base.LookAt();

        //GetComponentInChildren<MeshRenderer>().material = TVStatic;
    }

    public override void Interact()
    {
        base.Interact();
        

        uiGame.SetSubtitlesText(transcription);
        uiGame.PlaySubtitles(tapeOpenEffectSound.length, audioClip.length);

        FindObjectOfType<AudioManager>().PlaySound(tapeOpenEffectSound);
        FindObjectOfType<AudioManager>().PlaySound(audioClip, tapeOpenEffectSound.length);
        FindObjectOfType<AudioManager>().PlaySound(tapeCloseEffectSound, tapeOpenEffectSound.length + audioClip.length);
        

    }
}
