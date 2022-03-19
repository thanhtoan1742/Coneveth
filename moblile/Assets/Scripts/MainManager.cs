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

    [Header("Input and Error")]
    public GameObject loginForm;
    public GameObject error;


    protected MqttClientBehavior mqttClientBehavior;

    protected void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        mqttClientBehavior = gameObject.GetComponent<MqttClientBehavior>();
        mqttClientBehavior.ConnectionSucceeded += mqttOnConnectionSucceeded;
        mqttClientBehavior.ConnectionFailed += mqttOnConnectionFailed;
    }

    public void SubscribeTopic(string topic, Action<string> callback) {
        mqttClientBehavior.AddListenter(topic, callback);
    }

    public void PublishTopic(string topic, string message) {
        mqttClientBehavior.Publish(topic, message);
    }

    public void LoginConnect() {
        string brokerUri = brokerUriText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        string username = usernameText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        string password = passwordText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");


        brokerUri = "mqttserver.tk";
        brokerUri = "mqttserveraaa.tk";
        username = "bkiot";
        password = "12345678";

        mqttClientBehavior.brokerAddress = brokerUri;
        mqttClientBehavior.brokerPort = 1883;
        mqttClientBehavior.mqttUserName = username;
        mqttClientBehavior.mqttPassword = password;

        mqttClientBehavior.Connect();
    }

    public void LoginErrorBack() {
        error.SetActive(false);
        loginForm.SetActive(true);
    }

    public void mqttOnConnectionSucceeded() {
        Debug.Log("MQTT Connection Succeeded");
        SceneManager.LoadScene("Scenes/Dashboard");
    }

    public void mqttOnConnectionFailed() {
        Debug.Log("MQTT Connection Failed");
        loginForm.SetActive(false);
        error.SetActive(true);
    }
}
