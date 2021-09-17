using Business.Domain.Picking;
using MQTTnet.Client.Publishing;
using Picking.Hardware.Handler.Business;
using Picking.Hardware.Handler.Interface.Message;
using Picking.Hardware.Handler.MQTT;
using Picking.Hardware.Handler.Warehouse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Picking.Hardware.Handler.Services
{
    public class PickingFacePostman : IPickingFacePostman
    {

        private readonly MqttConnection mqttConn;
        private readonly ItemStockService stockService;

        public PickingFacePostman(MqttConnection _mqttConn
            , ItemStockService _stockService) {
            mqttConn = _mqttConn;
            stockService = _stockService;
        }

        public void FinishPicking(PickingItem lastItem, string sector) {
            var payload = new DeliveryMqttMessage(lastItem, "John Doe").FinishMessage(1);

            string itemStockCode = this.stockService.Get(sector, lastItem.SKU).StockCode;
            string topic = $"/tps/pickingface/{itemStockCode}";

            var result = this.mqttConn.Publish($"{topic}/sys", payload).GetAwaiter().GetResult();
            if (result.ReasonCode != MqttClientPublishReasonCode.Success) {
                Debug.WriteLine($"MQTT - FINISH - ERROR {result.ReasonCode.ToString()} - {result.ReasonString}");
            }
            mqttConn.UnSubscribe($"{topic}/esp").GetAwaiter();
        }

        public void PickManyRef(OrderPicking picking) {
            var validStatuses = new ItemStatus[]{
                ItemStatus.NO_READING,
                ItemStatus.PENDING,
                ItemStatus.PENDING_READING
            };
            var items = picking.Items.Where(i => validStatuses.Contains(i.Status));

            if (items.Any()) {


                items.GroupBy(i => i.SKU)
                    .ToList()
                    .ForEach(i => PickOneRef(i.ToList(), picking.Sector));
            }
        }

        public void PickOneRef(List<PickingItem> items, string sector) {
            var message = new DeliveryMqttMessage(items, "John Doe");

            string itemStockCode = this.stockService.Get(sector, items[0].SKU).StockCode;
            string topic = $"/tps/pickingface/{itemStockCode}";

            mqttConn.Subscribe($"{topic}/esp").GetAwaiter();

            var result = this.mqttConn.Publish($"{topic}/sys", message.PickMessage(1)).GetAwaiter().GetResult();
            if(result.ReasonCode != MqttClientPublishReasonCode.Success) {
                Debug.WriteLine($"MQTT - PICK - ERROR {result.ReasonCode.ToString()} - {result.ReasonString}");
            }
        }
    }
}
