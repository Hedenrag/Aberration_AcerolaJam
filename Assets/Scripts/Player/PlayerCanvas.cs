using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] GameObject interactItem;
    public void ShowInteractButton(bool show)
    {
        interactItem.SetActive(show);
    }
}
