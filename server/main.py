import time
import json
import paho.mqtt.client as mqtt


BROKER = 'mqttserver.tk'
PORT = 1883
USERNAME = 'bkiot'
PASSWORD = '12345678'
STATUS_TOPIC = '/bkiot/1910617/status'
LED_TOPIC = '/bkiot/1910617/led'
PUMP_TOPIC = '/bkiot/1910617/pump'

def on_connect(client, userdata, flags, rc):
    print('Connected with result code: ', rc)

    for topic in [STATUS_TOPIC, LED_TOPIC, PUMP_TOPIC]:
        client.subscribe(topic)

def on_message(client, userdata, msg):
    print(f'{msg.topic}: {msg.payload}')


def publish_status(client):
    # client.publish(STATUS_TOPIC, 'status'.encode(), 1)
    pass

def publish_led(client):
    data = json.dumps({
        'device': 'LED',
        'status': 'ON',
    })
    print(data)
    client.publish(LED_TOPIC, data)

def publish_pump(client):
    data = json.dumps({
        'device': 'PUMP',
        'status': 'OFF',
    })
    print(data)
    client.publish(PUMP_TOPIC, data)


if __name__ == '__main__':
    client = mqtt.Client()
    client.username_pw_set(USERNAME, PASSWORD)
    client.on_connect = on_connect
    client.on_message = on_message

    client.connect(BROKER, PORT)
    client.loop_start()

    while 1:
        # publish_status(client)
        # publish_led(client)
        # publish_pump(client)
        time.sleep(3)
