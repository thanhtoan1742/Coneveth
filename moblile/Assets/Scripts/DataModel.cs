using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PumpDataModel {
    public const string mqttTopic = "/bkiot/1910617/pump";
    public string device;
    public string status;

    public PumpDataModel(bool isOn = true) {
        device = "PUMP";
        status = isOn ? "ON" : "OFF";
    }

    public override string ToString() {
        return device + ": " + status;
    }
}


public class LedDataModel {
    public const string mqttTopic = "/bkiot/1910617/led";
    public string device;
    public string status;

    public LedDataModel(bool isOn = true) {
        device = "PUMP";
        status = isOn ? "ON" : "OFF";
    }

    public override string ToString() {
        return device + ": " + status;
    }
}


public class StatusDataModel {
    public const string mqttTopic = "/bkiot/1910617/status";

    public float temperature;
    public float humidity;
}