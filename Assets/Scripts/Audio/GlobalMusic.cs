using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMusic : MonoBehaviour
{
    static AudioSource musicHolder;

    [SerializeField] GameObject prefabMusic;
    [SerializeField, Range(0f,1f)] float targetVolume = 1.0f;

    public void PlayMusic(AudioClip audioClip) { PlayMusic(audioClip, 2f); }

    public void PlayMusic(AudioClip audioClip, float fadeTime)
    {
        if (musicHolder != null)
        {
            musicHolder.DOFade(0f, fadeTime).SetEase(Ease.InOutCirc).OnComplete(() => { Destroy(musicHolder.gameObject); });
        }

        GameObject newAudioObject = Instantiate(prefabMusic);
        DontDestroyOnLoad(newAudioObject);
        AudioSource newAudioSource = newAudioObject.GetComponent<AudioSource>();

        newAudioSource.loop = true;
        newAudioSource.volume = 0f;
        newAudioSource.DOFade(targetVolume, fadeTime).SetEase(Ease.InOutCirc);

        newAudioSource.clip = audioClip;
        newAudioSource.Play();
        musicHolder = newAudioSource;
    }

    public void StopMusic()
    {
        if (musicHolder != null)
        {
            musicHolder.DOFade(0f, 2f).SetEase(Ease.InOutCirc).OnComplete(() => { Destroy(musicHolder.gameObject); });
        }
        musicHolder = null;
    }
}