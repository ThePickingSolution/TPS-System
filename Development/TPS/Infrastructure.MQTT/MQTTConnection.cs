using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;
using MQTTnet.Client.Subscribing;
using MQTTnet.Client.Unsubscribing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MQTT
{
    public class MQTTConnection
    {
        public string ClientId { get; private set; }
        public string Server { get; private set; }
        public int Port { get; private set; }
        public bool IsConnected { get { return _client != null && _client.IsConnected;  } }

        public bool ListenerSetup { get; private set; }

        private IMqttClient _client;


        public MQTTConnection(string client, string server, int port, bool autoreconnect, bool start) {
            ClientId = client;
            Server = server;
            Port = port;

            _client = new MqttFactory().CreateMqttClient();

            // Set up handlers
            _client.ConnectedHandler = new MqttClientConnectedHandlerDelegate((args) => { Debug.WriteLine("MQTT Connected"); });
            _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate((args) => { Debug.WriteLine("MQTT DisConnected"); });

            if(autoreconnect)
                this.AutoReconnection();
            if (start)
                this.ConnectAsync().GetAwaiter().GetResult();
        }

        private IMqttClientOptions Options() {
            return new MqttClientOptionsBuilder()
                        .WithClientId(ClientId)
                        .WithTcpServer(Server, Port)
                        .Build();

        }


        public async Task<MqttClientAuthenticateResult> ConnectAsync() {
            return await _client.ConnectAsync(Options());
        }

        public async Task DisconnectAsync() {
            await _client.DisconnectAsync();
        }

        public void SetListener(Action<MqttApplicationMessageReceivedEventArgs> callback) {
            _client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(callback);
            ListenerSetup = true;
        }

        public void AutoReconnection() {
            _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate((args) => {
                Debug.WriteLine("MQTT Disconected");
                this.ConnectAsync().GetAwaiter().GetResult();
            });
        }

        public async Task<MqttClientPublishResult> Publish(string topic, string payload) {
            return await _client.PublishAsync(topic, payload, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce, false);
        }

        public async Task<MqttClientSubscribeResult> Subscribe(string topic) {
            return await _client.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);
        }

        public async Task<MqttClientUnsubscribeResult> UnSubscribe(string topic) {
            return await _client.UnsubscribeAsync(topic);
        }

    }
}
