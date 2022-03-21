using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TemperatureBarChartCard : MonoBehaviour {
    protected string mqttTopic = StatusDataModel.mqttTopic;
    protected BarChartCard card;

    protected void OnValueChange(string message) {
        // card.Add(float.Parse(message));
        var data = JsonConvert.DeserializeObject<StatusDataModel>(message);
        card.Add(data.temperature);
    }

    void Awake() {
        card = gameObject.GetComponent<BarChartCard>();
        card.unit = "°C";
        card.minValue = 0f;
        card.maxValue = 60f;
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
