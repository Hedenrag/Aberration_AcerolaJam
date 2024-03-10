using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    static List<CameraPosition> cameras = new();

    static CameraPosition currentBest = null;

    public static Camera Camera => RealCamera.GetCam;

    private void OnEnable()
    {
        cameras.Add(this);
        RealCamera.CreateCam();
    }
    private void OnDisable()
    {
        cameras.Remove(this);
    }

    public static CameraPosition GetBestCameraPosition(Transform source)
    {
        float bestPriority = float.MaxValue;
        CameraPosition best = currentBest;
        foreach (var cam in cameras)
        {
            Vector3 vect = cam.transform.position - source.position;
            float priority = Vector3.SqrMagnitude(vect) / Vector3.Dot(vect.normalized, source.forward) / Vector3.Dot(cam.transform.forward, source.forward);

            if (float.IsNegative(priority)) continue;

            if (priority < bestPriority) 
            { 
                bestPriority = priority;
                best = cam;
            };
        }
        currentBest = best;
        return best;
    }

}
