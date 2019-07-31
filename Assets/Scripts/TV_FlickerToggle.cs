using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV_FlickerToggle : MonoBehaviour
{
    public Material targetMaterial;
    public Renderer targetScreen;
    public TriggerVolume triggerVolume;
    public AudioClip audioClip;
    public bool autoReset = false;

    bool m_IsActive = false;
    float m_flickerTime = 1f;
    Material m_StartMaterial;

    private void Start()
    {
        m_StartMaterial = targetScreen.sharedMaterial;
    }


    // Update is called once per frame
    void Update()
    {
        if (triggerVolume.isTriggered && !m_IsActive)
        {
            m_IsActive = true;

            if (audioClip) AudioManager.PlaySound(audioClip);
        }

        if (m_IsActive)
        {
            targetScreen.sharedMaterial = targetMaterial;
            GetComponentInChildren<AudioSource>().Stop();
            GetComponentInChildren<Light>().enabled = false;
        }
    }
}
