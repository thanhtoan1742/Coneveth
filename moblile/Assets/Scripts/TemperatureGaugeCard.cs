using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGaugeCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.temperature";
    protected GaugeCard gaugeCard;

    protected void OnValueChange(string message) {
        gaugeCard.value = float.Parse(message);
    }

    void Awake() {
        gaugeCard = gameObject.GetComponent<GaugeCard>();
        gaugeCard.unit = "°C";
        gaugeCard.minValue = 0f;
        gaugeCard.maxValue = 60f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
