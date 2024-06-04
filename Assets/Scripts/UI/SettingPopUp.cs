using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingPopUp : MonoBehaviour
{
    public TMP_Dropdown graphicDropDown;
    public Slider masterSlider, musicSlider, sfxSlider, voiceSlider;
    public AudioMixer mixer;

    void Start()
    {
        graphicDropDown.value = QualitySettings.GetQualityLevel();
        if(mixer.GetFloat("MasterVolume", out float volume))
        {
            masterSlider.value = DecibelToLinear(volume);
        }
        if (mixer.GetFloat("MusicVolume", out volume))
        {
            musicSlider.value = DecibelToLinear(volume);
        }
        if (mixer.GetFloat("SFXVolume", out volume))
        {
            sfxSlider.value = DecibelToLinear(volume);
        }
        if (mixer.GetFloat("VoiceVolume", out volume))
        {
            voiceSlider.value = DecibelToLinear(volume);
        }
    }
    public void UpdateGraphicSetting(int level)
    {
        QualitySettings.SetQualityLevel(level);
    }
    public void SetVolume(string groupParma, float volume)
    {
        mixer.SetFloat(groupParma, LinearToDecibel(volume));
    }
    public void SetMasterVolume(float volume)
    {
        SetVolume("MasterVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        SetVolume("MusicVolume", volume);
    }
    public void SetSfxVolume(float volume)
    {
        SetVolume("SFXVolume", volume);
    }
    public void SetVoiceVolume(float volume)
    {
        SetVolume("VoiceVolume", volume);
    }
    float LinearToDecibel(float linear)
    {
        return Mathf.Clamp(20.0f * Mathf.Log10(linear), -80, 20);
    }
    float DecibelToLinear(float dB)
    {
        return Mathf.Pow(10.0f, dB / 20.0f);
    }
}
