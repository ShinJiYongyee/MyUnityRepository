using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SoundControler : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider BGMVolumeSlider;
    [SerializeField] private Slider SFXVolumeSlider;

    private void Awake()
    {
        MasterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        BGMVolumeSlider.onValueChanged.AddListener(SetBGMVolume);
        SFXVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

    }

    private void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master",Mathf.Log10(volume) * 20);

    }

    private void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);

    }
    private void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

    }

}
