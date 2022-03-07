using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityGaugeCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.humidity";
    protected GaugeCard gaugeCard;

    protected void OnValueChange(string message) {
        gaugeCard.value = float.Parse(message);
    }

    void Awake() {
        gaugeCard = gameObject.GetComponent<GaugeCard>();
        gaugeCard.unit = "%";
        gaugeCard.minValue = 0f;
        gaugeCard.maxValue = 100f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
