using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] Transform cameraT;
    List<Interactuable> interactuables = new();

    [SerializeField] PlayerCanvas playerCanvas;
    [SerializeField] CharacterControllerV2 playerInput;
    [SerializeField] PlayerCamera playerCamera;

    public void AddInteractuable(Interactuable iinteractuable)
    {
        interactuables.Add(iinteractuable);
        playerCanvas.ShowInteractButton(true);
    }

    public void RemoveInteractuable(Interactuable iinteractuable)
    {
        interactuables.Remove(iinteractuable);
        playerCanvas.ShowInteractButton(false);
    }

    public void BlockInput(bool block)
    {
        playerCamera.blockCamera = block;
        playerInput.blockInput = block;
    }
    public void BlockCamera(bool block) => playerCamera.blockCamera = block;
    public void BlockMovment(bool block) => playerInput.blockInput = block;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        float maxPriority = float.PositiveInfinity;
        Interactuable interactuable = null;
        foreach (var i in interactuables)
        {
            Vector3 vect = i.transform.position - cameraT.position;
            float priority = Vector3.SqrMagnitude(vect) / Vector3.Dot(vect.normalized, transform.forward);

            if (float.IsNegative(priority)) continue;

            if(priority < maxPriority)
            {
                maxPriority = priority;
                interactuable = i;
            }
        }
        if(interactuable != null)
        {
            interactuable.Interact(this);
        }
    }
}
