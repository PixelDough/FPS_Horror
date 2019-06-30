using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerToggle : MonoBehaviour
{
    public bool targetState;
    public Light lightToEffect;
    public TriggerVolume triggerVolume;
    public AudioClip audioClip;
    public bool autoReset = false;

    bool m_IsActive = false;
    float m_flickerTime = 1f;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (triggerVolume.isTriggered && !m_IsActive)
        {
            m_IsActive = true;

            if (audioClip) audioManager.PlaySound(audioClip);
        }

        if (m_IsActive)
        {
            if (m_flickerTime < 60f)
            {
                lightToEffect.gameObject.SetActive( (m_flickerTime % 10 < 5) ^ targetState );
            }
            else
            {
                lightToEffect.gameObject.SetActive( false ^ targetState );
            }

            m_flickerTime++;
        }
    }
}
