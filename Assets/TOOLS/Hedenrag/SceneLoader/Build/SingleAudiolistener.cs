using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[RequireComponent(typeof(AudioListener))]
public class SingleAudiolistener : MonoBehaviour
{
    AudioListener audioListener;
    private void Awake()
    {
        audioListener = GetComponent<AudioListener>();
    }
}
