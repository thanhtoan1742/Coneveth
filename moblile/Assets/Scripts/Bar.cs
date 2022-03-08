using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour {
    protected float _amount = 0.5f;
    public float amount {
        get { return _amount; }
        set {
            _amount = Mathf.Clamp01(value);
            UpdateUI();
        }
    }

    protected Color _color = Color.white;
    public Color color {
        get { return _color; }
        set {
            _color = value;
            UpdateUI();
        }
    }

    protected float fullHeight;

    protected RectTransform rectTransform;
    protected Image image;

    public void Awake() {
        fullHeight = gameObject.GetComponent<RectTransform>().rect.height;
        GameObject child = gameObject.transform.GetChild(0).gameObject;
        rectTransform = child.GetComponent<RectTransform>();
        image = child.GetComponent<Image>();
        UpdateUI();
    }

    void UpdateUI() {
        image.color = color;
        rectTransform.anchorMax = new Vector2(
            rectTransform.anchorMax.x,
            amount
        );
    }

}
