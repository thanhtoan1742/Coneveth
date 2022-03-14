using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Gauge : MonoBehaviour
{
    private static float guageEmpty = 0.097f;
    private static float guageFull = 0.91f;
    private float _amount = 0.5f;
    public float amount {
        get { return _amount; }
        set {
            _amount = Mathf.Clamp01(value);
            UpdateUI();
        }
    }

    protected Task tweenTask = null;


    protected GameObject background;
    protected GameObject filled;

    protected Image backgroundImage;
    protected Image filledImage;

    void Awake() {
        foreach (Transform child in gameObject.transform) {
            if (child.gameObject.name == "Filled")
                filled = child.gameObject;
            if (child.gameObject.name == "Background")
                background = child.gameObject;
        }
        backgroundImage = background.GetComponent<Image>();
        filledImage = filled.GetComponent<Image>();

        UpdateUI();
    }
    async void UpdateUI() {
        if (tweenTask != null && !tweenTask.IsCompleted)
            await tweenTask;
        tweenTask = DOTween.To(
            () => filledImage.fillAmount,
            (fa) => filledImage.fillAmount = fa,
            (guageFull - guageEmpty)*amount + guageEmpty,
            0.25f
        ).AsyncWaitForCompletion();
        // filledImage.fillAmount = (guageFull - guageEmpty)*amount + guageEmpty;
    }

}
