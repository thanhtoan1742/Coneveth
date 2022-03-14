using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChartCard : MonoBehaviour {
    public string unit = "%";
    protected BarChart barChart;
    public float minValue {
        get { return barChart.minValue; }
        set { barChart.minValue = value; }
    }
    public float maxValue {
        get { return barChart.maxValue; }
        set { barChart.maxValue = value; }
    }


    void Awake() {
        barChart = gameObject.GetComponentInChildren<BarChart>();
    }

    public void Add(float value) {
        barChart.Add(value);
    }
}
