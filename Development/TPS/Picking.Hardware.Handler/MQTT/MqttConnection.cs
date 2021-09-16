using Microsoft.Extensions.Options;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.MQTT
{
    public class MqttConnection
    {
        public string ClientId { get; private set; }
        public string Server { get; private set; }
        public int Port { get; private set; }


        private IMqttClient _client;

        
        public MqttConnection(string client,string server, int port) {
            ClientId = client;
            Server = server;
            Port = port;

            _client = new MqttFactory().CreateMqttClient();

            // Set up handlers
            _client.ConnectedHandler = new MqttClientConnectedHandlerDelegate((args) => { Console.WriteLine("MQTT Connected"); });
            _client.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate((args) => { Console.WriteLine("MQTT DisConnected"); });
        }

        private IMqttClientOptions Options() {
            return new MqttClientOptionsBuilder()
                        .WithClientId(ClientId)
                        .WithTcpServer(Server, Port)
                        .Build();

        }


        public void SetListener(Action<MqttApplicationMessageReceivedEventArgs> callback) {
            _client.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(callback);
        }


        public async Task<MqttClientAuthenticateResult> ConnectAsync() {
            return await _client.ConnectAsync(Options());
        }
        public async Task DisconnectAsync() {
            await _client.DisconnectAsync();
        }



        public async Task<MqttClientPublishResult> Publish(string topic, string payload) {
           return await _client.PublishAsync(topic, payload, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce, false);
        }

        public async Task<MqttClientSubscribeResult> Subscribe(string topic) {
            return await _client.SubscribeAsync(topic, MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce);
        }
        
        public async Task<MqttClientUnsubscribeResult> UnSubscribe(string topic) {
            return await _client.UnsubscribeAsync(topic);
        }
    }
}
