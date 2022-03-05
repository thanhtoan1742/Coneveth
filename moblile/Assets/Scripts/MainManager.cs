using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

    [Header("Text from input")]
    public GameObject brokerUriText;
    public GameObject usernameText;
    public GameObject passwordText;


    protected MqttClientBehavior mqttClientBehavior;

    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        mqttClientBehavior = gameObject.GetComponent<MqttClientBehavior>();
    }

    public void SubscribeTopic(string topic, Action<string> callback)
    {
        Debug.Log("subed to topic " + topic);
        mqttClientBehavior.AddSubscribedTopicCallbacks(topic, callback);
    }

    public void PublishTopic(string topic, string message)
    {
        mqttClientBehavior.Publish(topic, message);
    }

    public void ConnecButtonCallback()
    {
        string brokerUri = brokerUriText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        string username = usernameText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        string password = passwordText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");


        brokerUri = "io.adafruit.com";
        username = "thanhtoan1742";
        password = "aio_eVUG43XAwpj0GRPVkeSfvbvs4JTU";

        mqttClientBehavior.brokerAddress = brokerUri;
        // mqttClientBehavior.brokerPort = 1883;
        mqttClientBehavior.mqttUserName = username;
        mqttClientBehavior.mqttPassword = password;

        mqttClientBehavior.Connect();
        SceneManager.LoadScene("Scenes/Dashboard");
    }
}
