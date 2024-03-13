using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAudio : MonoBehaviour
{
    [SerializeField] GameObject AudioSource;

    public void PlayAudio(AudioClip audioclip)
    {
        GameObject g = Instantiate(AudioSource);
        DontDestroyOnLoad(g);
        AudioSource source = g.GetComponent<AudioSource>();

        source.clip = audioclip;
        source.Play();

        Destroy(g, audioclip.length);
    }
}