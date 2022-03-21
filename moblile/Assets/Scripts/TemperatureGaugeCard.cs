using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TemperatureGaugeCard : MonoBehaviour {
    protected string mqttTopic = StatusDataModel.mqttTopic;
    protected GaugeCard card;

    protected void OnValueChange(string message) {
        var data = JsonConvert.DeserializeObject<StatusDataModel>(message);
        card.value = data.temperature;
        // card.value = float.Parse(message);
    }

    void Awake() {
        card = gameObject.GetComponent<GaugeCard>();
        card.unit = "°C";
        card.minValue = 0f;
        card.maxValue = 60f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
