using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StrengthBarController : MonoBehaviour
{
    [SerializeField]
    protected Slider slider;

    public void SetStrength(float s)
    {
        slider.value = s;
    }
}
