using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GaugeCard : MonoBehaviour
{
    public float minValue = 0f;
    public float maxValue = 100f;
    public float value = 50f;
    public string unit = "%";


    protected Gauge gauge;
    protected TMP_Text valueText;

    protected Task task = null;

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
    }

    void UpdateUI()
    {
        valueText.text = string.Format("{0:0}", value) + unit;
        gauge.amount = (value - minValue)/(maxValue - minValue);
    }

    async void Update()
    {
        if (task != null)
            await task;
        value = Random.Range(minValue, maxValue);
        UpdateUI();
        task = Task.Delay(1000);
    }
}
