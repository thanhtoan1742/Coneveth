using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour
{
    // Start is called before the first frame update


    [Header("Colors")]
    public Color ativeColor;
    public Color inactiveColor;
    public Color knobColor;

    [Header("Animation duration")]
    public float animationDuration = 0.5f;



    protected GameObject leftEnd;
    protected GameObject middle;
    protected GameObject rightEnd;
    protected GameObject knob;


    protected RectTransform knobRectTransform;
    protected float middleWidth;
    protected Toggle toggle;

    private List<Task> tweenTasks = new List<Task>();

    protected void AssignChildGameObject()
    {
        leftEnd = gameObject.transform.GetChild(0).gameObject;
        middle = gameObject.transform.GetChild(1).gameObject;
        rightEnd = gameObject.transform.GetChild(2).gameObject;
        knob = gameObject.transform.GetChild(3).gameObject;
    }

    void Awake()
    {
        AssignChildGameObject();
        knobRectTransform = knob.GetComponent<RectTransform>();
        toggle = gameObject.GetComponent<Toggle>();
        middleWidth =
            gameObject.GetComponent<RectTransform>().rect.width
            - leftEnd.GetComponent<RectTransform>().rect.width
            - rightEnd.GetComponent<RectTransform>().rect.width;

        var middleRectTransform = middle.GetComponent<RectTransform>();
        middleRectTransform.sizeDelta = new Vector2(
            middleRectTransform.sizeDelta.x + middleWidth - middleRectTransform.rect.width,
            middleRectTransform.sizeDelta.y
        );

        foreach (Transform child in knob.transform)
        {
            child.gameObject.GetComponent<Image>().color = knobColor;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        Task.WaitAll(tweenTasks.ToArray());
        tweenTasks.Clear();

        var pos = new Vector2(
            toggle.isOn ? middleWidth/2 : -middleWidth/2,
            knobRectTransform.anchoredPosition.y
        );
        tweenTasks.Add(DOTween.To(
            () => knobRectTransform.anchoredPosition,
            x => knobRectTransform.anchoredPosition = x,
            pos,
            animationDuration
        ).AsyncWaitForCompletion());

        foreach (var g in new GameObject[]{leftEnd, middle, rightEnd})
        {
            tweenTasks.Add(DOTween.To(
                () => g.GetComponent<Image>().color,
                x => g.GetComponent<Image>().color = x,
                toggle.isOn ? ativeColor : inactiveColor,
                animationDuration
            ).AsyncWaitForCompletion());
        }
    }
}
