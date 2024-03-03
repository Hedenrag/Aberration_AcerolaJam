using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractBox : Interactuable
{
    [SerializeField] CinemachineVirtualCamera v_camera;

    [SerializeField] Camera r_camera;

    bool interacting = false;

    public override void Interact(PlayerInteractions playerInteractions)
    {
        if (interacting)
        {
            StopInteracting();
            playerInteractions.BlockInput(false);
        }
        else
        {
            StartInteracting();
            playerInteractions.BlockInput(true);
        }
    }

    

    void StartInteracting()
    {
        interacting = true;
        v_camera.Priority = 11;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    void StopInteracting()
    {
        interacting = false;
        v_camera.Priority = -100;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShiftHorizontal(float amount)
    {
        foreach (var puzzleItem in GenerateMesh.puzzleItems)
        {
            Vector3 pos = r_camera.WorldToViewportPoint(puzzleItem.transform.position);
            if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f || pos.z < 0f)
            {
                continue;
            }
            puzzleItem.HorizontalShift(r_camera.transform, amount);
        }
    }
    public void ShiftVertical(float amount)
    {
        foreach(var puzzleItem in GenerateMesh.puzzleItems)
        {
            Vector3 pos = r_camera.WorldToViewportPoint(puzzleItem.transform.position);
            if(pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f || pos.z <0f)
            {
                continue;
            }
            puzzleItem.VerticalShift(r_camera.transform, amount);
        }
    }
}
