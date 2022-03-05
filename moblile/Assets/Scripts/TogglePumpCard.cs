using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePumpCard : MonoBehaviour
{
    private string mqttTopic = "thanhtoan1742/feeds/baal.pump";
    private ButtonCard buttonCard;

    void Awake()
    {
        buttonCard = gameObject.GetComponent<ButtonCard>();
    }

    public void updateMqtt()
    {
        string message = buttonCard.isOn ? "ON" : "OFF";
        MainManager.instance.PublishTopic(mqttTopic, message);
    }
}
