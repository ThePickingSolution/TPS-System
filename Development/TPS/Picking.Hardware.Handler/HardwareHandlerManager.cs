
using Application.Picking.Interface.PickingItems;
using Business.Domain.Picking;
using MQTTnet.Client.Connecting;
using Picking.Hardware.Handler.Business;
using Picking.Hardware.Handler.Interface;
using Picking.Hardware.Handler.MQTT;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler
{
    public class HardwareHandlerManager : IHardwareHandlerManager
    {

        private readonly MqttConnection Connection;
        private readonly IHttpClientFactory factory;
        private readonly string picking_api = "localhost:53051";

        public HardwareHandlerManager(MqttConnection connection
            , IHttpClientFactory _factory) {
            Connection = connection;
            factory = _factory;
        }

        public bool Start() {
            try {
                var result = Connection.ConnectAsync().GetAwaiter().GetResult();
                var success  = result.ResultCode == MqttClientConnectResultCode.Success;

                if (!success)
                    Debug.WriteLine(result.ResultCode.ToString());
                else 
                    this.SetOnMessageReceive();


                return success;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }

        }

        public bool Stop() {
            try {
                var result = Connection.ConnectAsync().GetAwaiter().GetResult();
                var success = result.ResultCode == MqttClientConnectResultCode.Success;

                if (!success)
                    Debug.WriteLine(result.ResultCode.ToString());

                return success;

            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }


        private void SetOnMessageReceive() {
            this.Connection.SetListener((args) => {

                string payload= Encoding.UTF8.GetString(args.ApplicationMessage.Payload);

                Debug.WriteLine($"MQTT Received Message: {payload}");

                try {
                    var request = new HttpRequestMessage(
                HttpMethod.Post
                , $"http://{picking_api}/api/bymessage?payload={payload}");

                    request.Headers.Add("Accept", "application/json");
                    request.Content = new StringContent(payload);

                    var client = factory.CreateClient();
                    var response = client.Send(request);

                    Debug.WriteLine("MQTT Received Message - " + (response.IsSuccessStatusCode ? "PROCESSED" : "FAIL"));

                } catch (Exception) {
                    Debug.WriteLine("MQTT Received Message FAIL");


                }
            });
        }
    }
}
