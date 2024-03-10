using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerCameraControl : MonoBehaviour
{
    public static Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform;
    }
}
