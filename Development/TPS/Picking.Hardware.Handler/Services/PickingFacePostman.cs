using Business.Domain.Picking;
using MQTTnet.Client.Publishing;
using Picking.Hardware.Handler.Business;
using Picking.Hardware.Handler.Interface.Message;
using Picking.Hardware.Handler.MQTT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Picking.Hardware.Handler.Services
{
    public class PickingFacePostman : IPickingFacePostman
    {

        private readonly MqttConnection mqttConn;

        public PickingFacePostman(MqttConnection _mqttConn) {
            mqttConn = _mqttConn;
        }

        public void FinishPicking(PickingItem lastItem, string itemStockCode) {
            var payload = new DeliveryMqttMessage(lastItem, "John Doe").FinishMessage(1);

            var result = this.mqttConn.Publish($"{itemStockCode}/sys", payload).GetAwaiter().GetResult();
            if (result.ReasonCode != MqttClientPublishReasonCode.Success) {
                Debug.WriteLine($"MQTT - FINISH - ERROR {result.ReasonCode.ToString()} - {result.ReasonString}");
            }
            mqttConn.UnSubscribe($"{itemStockCode}/esp").GetAwaiter();
        }

        public void PickManyRef(OrderPicking picking, string itemStockCode) {
            var validStatuses = new ItemStatus[]{
                ItemStatus.NO_READING,
                ItemStatus.PENDING,
                ItemStatus.PENDING_READING
            };
            var items = picking.Items.Where(i => validStatuses.Contains(i.Status));

            if (items.Any()) {

                mqttConn.Subscribe($"{itemStockCode}/esp").GetAwaiter();

                items.GroupBy(i => i.SKU)
                    .ToList()
                    .ForEach(i => PickOneRef(i.ToList(), $"{itemStockCode}/sys"));
            }
        }

        public void PickOneRef(List<PickingItem> items, string itemStockCode) {
            var message = new DeliveryMqttMessage(items, "John Doe");
            var result = this.mqttConn.Publish(itemStockCode, message.PickMessage(1)).GetAwaiter().GetResult();
            if(result.ReasonCode != MqttClientPublishReasonCode.Success) {
                Debug.WriteLine($"MQTT - PICK - ERROR {result.ReasonCode.ToString()} - {result.ReasonString}");
            }
        }
    }
}
