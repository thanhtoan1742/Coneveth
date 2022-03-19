using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public class TestBehaviour : MonoBehaviour {
    protected string mqttTopic = "/bkiot/1910617/pump";
    protected void OnValueChange(string message) {
        var data = JsonConvert.DeserializeObject<PumpDataModel>(message);
        Debug.Log(data);
    }

    void Awake() {
        MainManager.instance.SubscribeTopic(mqttTopic, OnValueChange);
    }
}
