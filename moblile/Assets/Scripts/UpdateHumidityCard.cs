using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateHumidityCard : UpdateCard {
    private string mqttTopic = "thanhtoan1742/feeds/baal.humidity";

    protected void UpdateHumidity(string message)
    {
        value = message;
    }

    protected override void Start()
    {
        MainManager.instance.SubscribeTopic(mqttTopic, UpdateHumidity);
        unit = "%";
        base.Start();
    }

}
