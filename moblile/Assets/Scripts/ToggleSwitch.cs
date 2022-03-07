using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour {
    // Start is called before the first frame update


    [Header("Colors")]
    public Color ativeColor;
    public Color inactiveColor;
    public Color knobColor;

    [Header("Animation duration")]
    public float animationDuration = 0.5f;


    protected Image leftImage;
    protected Image rightImage;
    protected Image middleImage;
    protected RectTransform knobRectTransform;


    protected Toggle toggle;
    protected float middleWidth;


    private List<Task> tweenTasks = new List<Task>();


    void Awake() {
        RectTransform middleRectTransform = null;
        GameObject knob = null;

        middleWidth = gameObject.GetComponent<RectTransform>().rect.width;
        foreach (Transform child in gameObject.transform) {
            if (child.gameObject.name == "Left") {
                leftImage = child.gameObject.GetComponent<Image>();
                middleWidth -= child.gameObject.GetComponent<RectTransform>().rect.width;
            }
            if (child.gameObject.name == "Right") {
                rightImage = child.gameObject.GetComponent<Image>();
                middleWidth -= child.gameObject.GetComponent<RectTransform>().rect.width;
            }
            if (child.gameObject.name == "Middle") {
                middleImage = child.gameObject.GetComponent<Image>();
                middleRectTransform = child.gameObject.GetComponent<RectTransform>();
            }
            if (child.gameObject.name == "Knob") {
                knob = child.gameObject;
                knobRectTransform = knob.GetComponent<RectTransform>();
            }
        }
        toggle = gameObject.GetComponent<Toggle>();

        middleRectTransform.sizeDelta = new Vector2(
            middleRectTransform.sizeDelta.x + middleWidth - middleRectTransform.rect.width,
            middleRectTransform.sizeDelta.y
        );

        foreach (Transform child in knob.transform) {
            child.gameObject.GetComponent<Image>().color = knobColor;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        Task.WaitAll(tweenTasks.ToArray());
        tweenTasks.Clear();

        var newKnobPosition = new Vector2(
            toggle.isOn ? middleWidth/2 : -middleWidth/2,
            knobRectTransform.anchoredPosition.y
        );
        tweenTasks.Add(DOTween.To(
            () => knobRectTransform.anchoredPosition,
            position => knobRectTransform.anchoredPosition = position,
            newKnobPosition,
            animationDuration
        ).AsyncWaitForCompletion());

        foreach (var image in new Image[]{leftImage, middleImage, rightImage})
        {
            tweenTasks.Add(DOTween.To(
                () => image.color,
                color => image.color = color,
                toggle.isOn ? ativeColor : inactiveColor,
                animationDuration
            ).AsyncWaitForCompletion());
        }
    }
}
