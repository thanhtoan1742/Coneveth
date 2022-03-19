using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LedToggleSwitchCard : MonoBehaviour {
    public const string mqttTopic = LedDataModel.mqttTopic;
    protected ToggleSwitchCard card;

    void Awake() {
        card = gameObject.GetComponent<ToggleSwitchCard>();
        card.AddListener((value) => {
            string message = JsonConvert.SerializeObject(new LedDataModel(value));
            Debug.Log(message);
            MainManager.instance.PublishTopic(mqttTopic, message);
        });
    }
}
