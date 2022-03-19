using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class MqttClientBehavior : M2MqttUnityClient
{
    // every topics that is used have to be put in here first.
    protected List<string> topics = new List<string>{
        LedDataModel.mqttTopic,
        PumpDataModel.mqttTopic,
        StatusDataModel.mqttTopic,
    };
    protected Dictionary<string, List<Action<string>>> listeners = new Dictionary<string, List<Action<string>>>();

    public void AddListenter(string topic, Action<string> listener) {
        Debug.Assert(topics.Contains(topic), $"{topic} not decleared in MainManager");
        listeners[topic].Add(listener);
    }

    public void Publish(string topic, string message) {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    protected override void SubscribeTopics() {
        foreach (string topic in topics) {
            client.Subscribe(new string[] {topic}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
    }

    protected override void UnsubscribeTopics() {
        foreach (string topic in topics) {
            client.Unsubscribe(new string[] {topic});
        }
    }

    protected override void OnConnectionFailed(string errorMessage) {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void DecodeMessage(string topic, byte[] message) {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Assert(topics.Contains(topic), $"{topic} not decleared in MainManager");
        Debug.Log($"DECODE MESSAGE:: {topic}: {msg}");
        foreach (Action<string> action in listeners[topic]) {
            action(msg);
        }
    }

    private void OnDestroy() {
        Disconnect();
    }


    protected override void Awake() {
        base.Awake();
        timeoutOnConnection = 5000; // 5s time out
        foreach(string topic in topics)
            listeners[topic] = new List<Action<string>>();
    }
}
