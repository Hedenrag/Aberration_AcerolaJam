using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    protected void OnTriggerEnter(Collider other)
    {
        var playerInteractor = other.gameObject.GetComponent<PlayerInteractions>();
        if (playerInteractor == null) return;
        playerInteractor.AddInteractuable(this);
    }
    protected void OnTriggerExit(Collider other)
    {
        var playerInteractor = other.gameObject.GetComponent<PlayerInteractions>();
        if (playerInteractor == null) return;
        playerInteractor.RemoveInteractuable(this);
    }

    public virtual void Interact(PlayerInteractions playerInteractions)
    {
        Debug.Log($"{playerInteractions} Interacting with {name} in {gameObject.name}", gameObject);
    }
}
