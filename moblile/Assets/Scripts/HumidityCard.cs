using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class HumidityCard : MonoBehaviour {
    protected string mqttTopic = StatusDataModel.mqttTopic;
    protected Card card;

    protected void OnValueChange(string message) {
        var data = JsonConvert.DeserializeObject<StatusDataModel>(message);
        card.value = data.humidity.ToString();
    }

    void Awake() {
        card = gameObject.GetComponent<Card>();
        card.unit = "%";
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }

}
