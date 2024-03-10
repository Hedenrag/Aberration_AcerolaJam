using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCamera : MonoBehaviour
{
    static Camera cam;
    public static Camera GetCam
    {
        get
        {
            if(cam == null) 
            { 
                cam = ScriptableObjectInstanceCreateCam.Instance.CreateCam().GetComponent<Camera>();
            }
            return cam;
        }
    }
    public static void CreateCam()
    {
        if (cam != null) return;
        cam = ScriptableObjectInstanceCreateCam.Instance.CreateCam().GetComponent<Camera>();
    }

    private void Update()
    {
        CameraPosition camPos = CameraPosition.GetBestCameraPosition(CameraPlayerCameraControl.playerTransform);
        if(camPos == null ) { return; }
        transform.position = camPos.transform.position;
        transform.rotation = camPos.transform.rotation;
    }
}
