using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCard : MonoBehaviour
{
    public GameObject slider;
    protected RectTransform sliderRectTransform;
    public bool isOn = false;

    void Awake()
    {
        sliderRectTransform = slider.GetComponent<RectTransform>();
    }

    public void Toggle()
    {
        var x = sliderRectTransform.anchoredPosition.x;
        var y = sliderRectTransform.anchoredPosition.y;
        sliderRectTransform.anchoredPosition = new Vector2(-x, y);
        isOn = !isOn;
    }
}
