using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrengthBarController : MonoBehaviour
{
    [SerializeField]
    protected Slider slider;
    public float MaxValue { get { return slider.maxValue; } }
    public float MinValue { get { return slider.minValue; } }

    public void SetStrength(float s)
    {
        slider.value = s;
    }

    public void ResetStrength()
    {
        slider.value = slider.maxValue;
    }

}
