using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class SettingsMenu : MonoBehaviour
{
    [Header("Graphics Settings")]
    public TMPro.TMP_Dropdown resolutionDropdown;

    [Space]
    public Toggle motionBlurToggle;
    public Toggle depthOfFieldToggle;
    public Toggle grainToggle;

    [Header("Audio Settings")]
    public AudioMixer audioMixer;

    [Space]
    public Slider mainVolumeSlider;
    public TMPro.TMP_Text mainVolumeNumber;

    [Space]
    public Slider ambienceVolumeSlider;
    public TMPro.TMP_Text ambienceVolumeNumber;

    [Space]
    public Slider soundsVolumeSlider;
    public TMPro.TMP_Text soundsVolumeNumber;

    [Header("Gameplay Settings")]
    public Slider lookSensitivitySlider;
    public TMPro.TMP_Text lookSensitivityNumber;

    Resolution[] resolutions;

    MotionBlur motionBlur;
    DepthOfField depthOfField;
    Grain grain;

    void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        float mainVolume;
        float ambienceVolume;
        float soundsVolume;
        audioMixer.GetFloat("volumeMaster", out mainVolume);
        audioMixer.GetFloat("volumeAmbience", out ambienceVolume);
        audioMixer.GetFloat("volumeSounds", out soundsVolume);

        mainVolumeSlider.value = mainVolume;
        ambienceVolumeSlider.value = ambienceVolume;
        soundsVolumeSlider.value = soundsVolume;

        lookSensitivitySlider.value = GameManager.Instance.lookSensitivity;


        // Post processing
        PostProcessVolume volume = FindObjectOfType<PostProcessVolume>().GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out motionBlur);
        volume.profile.TryGetSettings(out depthOfField);
        volume.profile.TryGetSettings(out grain);
    }

    void Update()
    {
        mainVolumeNumber.text = ((mainVolumeSlider.value)).ToString();
        ambienceVolumeNumber.text = ((ambienceVolumeSlider.value)).ToString();
        soundsVolumeNumber.text = ((soundsVolumeSlider.value)).ToString();

        lookSensitivityNumber.text = lookSensitivitySlider.value.ToString();

        motionBlurToggle.isOn = motionBlur.active;
        depthOfFieldToggle.isOn = depthOfField.active;
        grainToggle.isOn = grain.active;
    }

    public void SetResolution(int resolutionIndex)
    { 
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolumeMaster(float volume)
    {
        //audioMixer.SetFloat("volumeMaster", (((volume - 0) * (0 - -80)) / (100 - 0)) + -80);
        audioMixer.SetFloat("volumeMaster", volume);
    }

    public void SetVolumeSounds(float volume)
    {
        //audioMixer.SetFloat("volumeSounds", (((volume - 0) * (0 - -80)) / (100 - 0)) + -80);
        audioMixer.SetFloat("volumeSounds", volume);
    }

    public void SetVolumeAmbience(float volume)
    {
        //audioMixer.SetFloat("volumeAmbience", (((volume - 0) * (0 - -80)) / (100 - 0)) + -80);
        audioMixer.SetFloat("volumeAmbience", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool state)
    {
        Screen.fullScreen = state;
    }

    public void SetVSync(bool state)
    {
        QualitySettings.vSyncCount = state ? 1 : 0;
    }

    public void SetMotionBlur(bool state)
    {
        motionBlur.active = state;
    }

    public void SetDepthOfField(bool state)
    {
        depthOfField.active = state;
    }

    public void SetGrain(bool state)
    {
        grain.active = state;
    }

    public void SetLookSensitivity(float sensitivity)
    {
        GameManager.Instance.lookSensitivity = sensitivity;
    }
}
