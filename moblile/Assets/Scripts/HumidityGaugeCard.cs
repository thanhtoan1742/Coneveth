using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class HumidityGaugeCard : MonoBehaviour {
    protected string mqttTopic = StatusDataModel.mqttTopic;
    protected GaugeCard card;

    protected void OnValueChange(string message) {
        var data = JsonConvert.DeserializeObject<StatusDataModel>(message);
        card.value = data.humidity;
    }

    void Awake() {
        card = gameObject.GetComponent<GaugeCard>();
        card.unit = "%";
        card.minValue = 0f;
        card.maxValue = 100f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
