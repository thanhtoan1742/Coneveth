using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureBarChartCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.temperature";
    protected BarChartCard card;

    protected void OnValueChange(string message) {
        card.Add(float.Parse(message));
    }

    void Awake() {
        card = gameObject.GetComponent<BarChartCard>();
        card.unit = "°C";
        card.minValue = 0f;
        card.maxValue = 60f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
