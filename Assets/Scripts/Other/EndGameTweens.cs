using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTweens : MonoBehaviour
{

    [SerializeField] CanvasGroup EndImage;
    [SerializeField] CanvasGroup BackButton;

    void Start()
    {
        EndImage.DOFade(1f, 3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            BackButton.DOFade(1f, 1.5f).SetEase(Ease.InCirc).OnComplete(() => 
            {
                BackButton.interactable = true;
            
            });
        });
    }
}
