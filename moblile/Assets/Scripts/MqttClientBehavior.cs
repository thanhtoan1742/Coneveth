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
        "thanhtoan1742/feeds/baal.temperature",
        "thanhtoan1742/feeds/baal.humidity",
    };
    protected Dictionary<string, List<Action<string>>> subscribedTopicCallbacks = new Dictionary<string, List<Action<string>>>();

    public void AddSubscribedTopicCallbacks(string topic, Action<string> callback)
    {
        Debug.Assert(topics.Contains(topic), $"{topic} not decleared in MainManager");

        if (!subscribedTopicCallbacks.ContainsKey(topic))
            subscribedTopicCallbacks[topic] = new List<Action<string>>();
        subscribedTopicCallbacks[topic].Add(callback);
    }

    public void Publish(string topic, string message)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    protected override void SubscribeTopics()
    {
        foreach (string topic in topics)
        {
            client.Subscribe(new string[] {topic}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }
    }

    protected override void UnsubscribeTopics()
    {
        foreach (string topic in topics)
        {
            client.Unsubscribe(new string[] {topic});
        }
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
        Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected to broker on " + brokerAddress + "\n");
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.");
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("CONNECTION LOST!");
    }


    protected override void DecodeMessage(string topic, byte[] message)
    {
        string msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log(topic + ": " + msg);
        foreach (Action<string> action in subscribedTopicCallbacks[topic])
        {
            action(msg);
        }
    }


    private void OnDestroy()
    {
        Disconnect();
    }


    private void OnValidate()
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
