using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.humidity";
    protected Card card;

    protected void OnValueChange(string message) {
        card.value = message;
    }

    void Awake() {
        card = gameObject.GetComponent<Card>();
        card.unit = "%";
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }

}
