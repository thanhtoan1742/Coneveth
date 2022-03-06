using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonCard : MonoBehaviour
{
    public bool isOn = false;

    public GameObject slider;
    protected RectTransform sliderRectTransform;
    private Task tweenCompleted = null;


    private Vector2 GetPosition(bool isOn)
    {
        var x = sliderRectTransform.anchoredPosition.x;
        x = Math.Abs(x);
        var y = sliderRectTransform.anchoredPosition.y;
        return new Vector2(isOn ? x : -x, y);
    }

    void Awake()
    {
        sliderRectTransform = slider.GetComponent<RectTransform>();
        sliderRectTransform.anchoredPosition = GetPosition(isOn);
    }

    public async void Toggle()
    {
        if (tweenCompleted != null)
        {
            await tweenCompleted;
        }

        isOn = !isOn;
        tweenCompleted = DOTween.To(
            () => sliderRectTransform.anchoredPosition,
            pos => sliderRectTransform.anchoredPosition = pos,
            GetPosition(isOn),
            (float)(0.25)
        ).AsyncWaitForCompletion();
    }
}
