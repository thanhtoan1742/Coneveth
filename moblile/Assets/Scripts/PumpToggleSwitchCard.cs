using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PumpToggleSwitchCard : MonoBehaviour {
    public const string mqttTopic = PumpDataModel.mqttTopic;
    protected ToggleSwitchCard card;

    void Awake() {
        card = gameObject.GetComponent<ToggleSwitchCard>();
        card.AddListener((value) => {
            string message = JsonConvert.SerializeObject(new PumpDataModel(value));
            Debug.Log(message);
            MainManager.instance.PublishTopic(mqttTopic, message);
        });
    }
}
