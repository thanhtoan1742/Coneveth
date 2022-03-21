using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class TemperatureCard : MonoBehaviour {
    protected string mqttTopic = StatusDataModel.mqttTopic;
    protected Card card;

    protected void OnValueChange(string message) {
        var data = JsonConvert.DeserializeObject<StatusDataModel>(message);
        card.value = data.temperature.ToString();
    }

    void Awake() {
        card = gameObject.GetComponent<Card>();
        card.unit = "°C";
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }

}
