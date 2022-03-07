using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeCard : MonoBehaviour
{
    public string unit = "%";
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
    protected float _value = 50f;
    public float value {
        get { return _value; }
        set {
            _value = value;
            UpdateUI();
        }
    }


    protected Gauge gauge;
    protected TMP_Text valueText;
    protected TMP_Text minText;
    protected TMP_Text maxText;


    void Awake() {
        foreach (Transform child in gameObject.transform) {
            if (child.gameObject.name == "Gauge") {
                gauge = child.gameObject.GetComponent<Gauge>();
                valueText = child.Find("Value").gameObject.GetComponent<TMP_Text>();
            }
            if (child.gameObject.name == "Min")
                minText = child.gameObject.GetComponent<TMP_Text>();
            if (child.gameObject.name == "Max")
                maxText = child.gameObject.GetComponent<TMP_Text>();
        }

        UpdateMinMaxText();
        UpdateUI();
    }

    void UpdateMinMaxText() {
        minText.text = minValue.ToString();
        maxText.text = maxValue.ToString();
    }

    void UpdateUI() {
        valueText.text = string.Format("{0:0}", value) + unit;
        gauge.amount = (value - minValue)/(maxValue - minValue);
    }
}
