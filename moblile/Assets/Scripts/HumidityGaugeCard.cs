using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityGaugeCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.humidity";
    protected GaugeCard card;

    protected void OnValueChange(string message) {
        card.value = float.Parse(message);
    }

    void Awake() {
        card = gameObject.GetComponent<GaugeCard>();
        card.unit = "%";
        card.minValue = 0f;
        card.maxValue = 100f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
