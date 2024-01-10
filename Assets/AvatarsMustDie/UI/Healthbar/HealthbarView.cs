using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarView : MonoBehaviour
{
    public float MaxValue => slider.maxValue;
    public float CurrentValue => slider.value;
    
    [SerializeField] private Slider slider;

    public void SetCurrentValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }
}
