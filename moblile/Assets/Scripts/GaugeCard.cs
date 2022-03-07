using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeCard : MonoBehaviour
{
    public string unit = "%";
    public float minValue = 0f;
    public float maxValue = 100f;
    private float _value = 50f;
    public float value {
        get { return _value; }
        set {
            _value = value;
            UpdateUI();
        }
    }


    protected Gauge gauge;
    protected TMP_Text valueText;


    void Awake()
    {
        TMP_Text minText = null, maxText = null;
        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.name == "Gauge")
            {
                gauge = child.gameObject.GetComponent<Gauge>();
                valueText = child.Find("Value").gameObject.GetComponent<TMP_Text>();
            }
            if (child.gameObject.name == "Min")
                minText = child.gameObject.GetComponent<TMP_Text>();
            if (child.gameObject.name == "Max")
                maxText = child.gameObject.GetComponent<TMP_Text>();
        }

        minText.text = minValue.ToString();
        maxText.text = maxValue.ToString();

        UpdateUI();
    }

    void UpdateUI() {
        valueText.text = string.Format("{0:0}", value) + unit;
        gauge.amount = (value - minValue)/(maxValue - minValue);
    }
}
