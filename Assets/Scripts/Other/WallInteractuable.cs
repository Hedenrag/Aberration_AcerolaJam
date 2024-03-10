using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteractuable : Interactuable
{
    [SerializeField] Animator wallAnimator;
    [SerializeField] GameObject[] gameObjectsToDestroy;
    public override void Interact(PlayerInteractions playerInteractions)
    {
        wallAnimator.SetTrigger("DoAnimation");
        playerInteractions.RemoveInteractuable(this);
        foreach (var gameObject in gameObjectsToDestroy)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
