using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputValueProcessor : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Slider slider;

    [SerializeField] Ease sliderEase = Ease.OutCubic;

    public void OnInputChange(string input)
    {
        float value = float.Parse(input);
        value = Mathf.Clamp(value, 0f, 100f);
        var text = value.ToString("0.##");
        text += "%";
        inputField.SetTextWithoutNotify(text);
        value /= 100f;
        slider.DOValue(value, 0.5f).SetEase(sliderEase);
    }

    public void OnSliderChanged(float value)
    {
        value *= 100f;
        var text = value.ToString("0.##");
        text += "%";
        inputField.SetTextWithoutNotify(text);
    }
}
