using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class TemperatureCard : Card {
    private string mqttTopic = "thanhtoan1742/feeds/baal.temperature";
    private Card card;

    protected void OnValueChange(string message) {
        card.value = message;
    }

    void Awake() {
        card = gameObject.GetComponent<Card>();
        card.unit = "°C";
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }

}
