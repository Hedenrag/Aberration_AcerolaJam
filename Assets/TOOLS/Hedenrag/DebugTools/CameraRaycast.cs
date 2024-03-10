using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    Camera cam;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            if(Physics.Raycast(cam.ScreenPointToRay(mousePos), out RaycastHit hit))
            {
                Debug.Log(hit.collider,hit.collider.gameObject);
            }
        }
    }
}
