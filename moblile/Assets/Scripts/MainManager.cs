using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using TMPro;

public class MainManager : MonoBehaviour {
    public static MainManager instance = null;

    [Header("Text from input")]
    public GameObject brokerUriText;
    public GameObject usernameText;
    public GameObject passwordText;

    [Header("Input and Error")]
    public GameObject loginForm;
    public GameObject error;


    protected string brokerAddress;
    protected int brokerPort;
    protected bool isEncrypted;
    protected string username;
    protected string password;
    protected MqttClient client;
    protected Dictionary<string, List<Action<string>>> listeners;
    protected List<MqttMsgPublishEventArgs> frontMessageQueue;
    protected List<MqttMsgPublishEventArgs> backMessageQueue;


    protected void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        listeners = new Dictionary<string, List<Action<string>>>();
        frontMessageQueue = new List<MqttMsgPublishEventArgs>();
        backMessageQueue = new List<MqttMsgPublishEventArgs>();
    }

    protected void OnDestroy() {
        if (client != null) {
            string[] keys = new string[listeners.Keys.Count];
            listeners.Keys.CopyTo(keys, 0);
            client.Unsubscribe(keys);
        }
    }

    public void SubscribeTopic(string topic, Action<string> listener) {
        if (!listeners.ContainsKey(topic)) {
            listeners[topic] = new List<Action<string>>();
            client.Subscribe(
                new string[] { topic },
                new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE }
            );
        }
        listeners[topic].Add(listener);
    }

    public void PublishTopic(string topic, string message) {
        client.Publish(
            topic,
            System.Text.Encoding.UTF8.GetBytes(message),
            MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE,
            false
        );
    }

    protected void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e) {
        frontMessageQueue.Add(e);
    }

    protected void MqttConnect() {
        bool connected = false;
        try {
            client = new MqttClient(brokerAddress, brokerPort, isEncrypted, null, null, isEncrypted ? MqttSslProtocols.SSLv3 : MqttSslProtocols.None);
            client.MqttMsgPublishReceived += MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            client.Connect(clientId, username, password);
            connected = client.IsConnected;
        }
        catch (Exception e) {
            Debug.Log(e.Message);
            connected = false;
        }

        if (connected) {
            Debug.Log("MQTT Connection Succeeded");
            SceneManager.LoadScene("Scenes/Dashboard");
        } else {
            Debug.Log("MQTT Connection Failed");
            loginForm.SetActive(false);
            error.SetActive(true);
            client = null;
        }
    }

    void ProcessMqttEvents() {
        foreach (var e in backMessageQueue) {
            string topic = e.Topic;
            string message = System.Text.Encoding.UTF8.GetString(e.Message);
            foreach (var listener in listeners[topic])
                listener(message);
        }
        backMessageQueue.Clear();
    }

    void Update() {
        var tempQueue = frontMessageQueue;
        frontMessageQueue = backMessageQueue;
        backMessageQueue = tempQueue;

        ProcessMqttEvents();
    }

    public void LoginConnect() {
        brokerAddress = brokerUriText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        username = usernameText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        password = passwordText.GetComponent<TMP_Text>().text.Trim().Replace("\u200B", "");
        isEncrypted = false;
        brokerPort = 1883;

        // brokerAddress = "mqttserver.tk";
        // brokerAddress = "mqttserveraaa.tk";
        // username = "bkiot";
        // password = "12345678";
        MqttConnect();
    }

    public void LoginErrorBack() {
        error.SetActive(false);
        loginForm.SetActive(true);
        client = null;
    }
}
