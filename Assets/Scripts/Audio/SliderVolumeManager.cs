using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SliderVolumeManager : MonoBehaviour
{
    [SerializeField] Slider slider;

    const float a = 0.00005f;
    const float b = 11.51f;

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string settingName;

    private void OnEnable()
    {
        LoadSetting();
    }

    private void OnDisable()
    {
        SaveSetting();
    }

    public void SetVolume(float value)
    {
        audioMixer.SetFloat(settingName, SliderToVolumeFormula(value));
    }

    public float GetVolume()
    {
        float volume;
        audioMixer.GetFloat(settingName, out volume);

        return volume;
    }

    public void SaveSetting()
    {
        audioMixer.GetFloat(settingName, out float value);
        PlayerPrefs.SetFloat(settingName, value);
    }
    public void LoadSetting()
    {
        float value = PlayerPrefs.GetFloat(settingName);
        audioMixer.SetFloat(settingName, value);
    }


    private void OnValidate()
    {
        if (slider == null) slider = GetComponent<Slider>();
    }

    private void Start()
    {
        SetSliderPos();
    }

    public void SetSliderPos()
    {
        slider.value = VolumeToSliderFormula(GetVolume());
    }

    float VolumeToSliderFormula(float volume)
    {
        volume = (volume + 80f) / 100f;

        volume = a * Mathf.Exp(b * volume);

        return volume;
    }
    float SliderToVolumeFormula(float volume)
    {
        if (volume < 0.01)
        {
            volume = 0;
        }
        else
        {
            volume = Mathf.Log(volume / a) / b;
        }

        volume = (volume * 100f) - 80f;
        return volume;
    }
}