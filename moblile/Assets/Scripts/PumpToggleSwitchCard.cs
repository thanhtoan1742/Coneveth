using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpToggleSwitchCard : MonoBehaviour {
    protected string mqttTopic = "thanhtoan1742/feeds/baal.pump";
    protected ToggleSwitchCard card;

    void Awake() {
        card = gameObject.GetComponent<ToggleSwitchCard>();
        card.AddListener((value) => {
            var message = value ? "ON" : "OFF";
            MainManager.instance.PublishTopic(mqttTopic, message);
        });
    }
}
