using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBarScript : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;
    [SerializeField]
    private Image fill;

    public void SetMaxFuel(int fuel)
    {
        slider.maxValue = fuel;
        slider.value = fuel;

        fill.color = gradient.Evaluate(1.0f);
    }

    public void SetFuel(int fuel)
    {
        slider.value = fuel;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
