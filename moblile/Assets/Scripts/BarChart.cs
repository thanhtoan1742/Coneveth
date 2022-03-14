using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BarChart : MonoBehaviour {
    public GameObject barPrefab;
    public int capacity = 10;
    protected GameObject barsGameObject;
    protected TMP_Text minText;
    protected TMP_Text maxText;

    protected float _minValue = 0f;
    public float minValue {
        get { return _minValue; }
        set {
            _minValue = value;
            UpdateMinMaxText();
        }
    }
    protected float _maxValue = 100f;
    public float maxValue {
        get { return _maxValue; }
        set {
            _maxValue = value;
            UpdateMinMaxText();
        }
    }

    protected BarChart barChart;

    void Awake() {
        foreach (Transform child in gameObject.transform) {
            if (child.gameObject.name == "MinText")
                minText = child.gameObject.GetComponent<TMP_Text>();
            if (child.gameObject.name == "MaxText")
                maxText = child.gameObject.GetComponent<TMP_Text>();
            if (child.gameObject.name == "Foreground")
                barsGameObject = child.Find("Bars").gameObject;
        }
        UpdateMinMaxText();
        UpdateUI();
    }

    void UpdateMinMaxText() {
        minText.text = minValue.ToString();
        maxText.text = maxValue.ToString();
    }

    GameObject CreateBar(float value) {
        GameObject barGameObject = Instantiate(barPrefab);
        barGameObject.GetComponent<Bar>().amount = (value - minValue)/(maxValue - minValue);
        return barGameObject;
    }

    public void Add(float value) {
        while (barsGameObject.transform.childCount >= capacity) {
            Transform child = barsGameObject.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        GameObject g = CreateBar(value);
        g.transform.SetParent(barsGameObject.transform, false);
        g.transform.SetAsLastSibling();
        UpdateUI();
    }

    void UpdateUI() {
        int cnt = 0;
        foreach (Transform child in barsGameObject.transform) {
            var rectTransform = child.gameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1.0f/capacity*cnt, 0);
            rectTransform.anchorMax = new Vector2(1.0f/capacity*(cnt + 1), 1);
            cnt += 1;
        }
    }
}
