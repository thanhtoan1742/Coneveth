using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureGaugeCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.temperature";
    protected GaugeCard card;

    protected void OnValueChange(string message) {
        card.value = float.Parse(message);
    }

    void Awake() {
        card = gameObject.GetComponent<GaugeCard>();
        card.unit = "°C";
        card.minValue = 0f;
        card.maxValue = 60f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
