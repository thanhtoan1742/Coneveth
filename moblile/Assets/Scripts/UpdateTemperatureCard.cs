using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateTemperatureCard : UpdateCard {
    private string mqttTopic = "thanhtoan1742/feeds/baal.temperature";

    protected void UpdateTemperature(string message)
    {
        value = message;
    }

    protected override void Start()
    {
        MainManager.instance.SubscribeTopic(mqttTopic, UpdateTemperature);
        unit = "°C";
        base.Start();
    }

}
