using Business.Domain.Picking;
using Business.Domain.Warehouse.Stock;
using Infrastructure.MQTT;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Publishing;
using Service.PickToLight.Interface;
using Service.PickToLight.Interface.Warehouse;
using Service.PickToLight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight
{
    public class PickingFaceService : IPickingFaceService
    {
        private readonly MQTTConnection MqttConn;
        private readonly IItemStockProxyRepository ItemStockRepository;
        public PickingFaceService(MQTTConnection mqttConn
            , IItemStockProxyRepository itemStockRepository) {
            MqttConn = mqttConn;
            ItemStockRepository = itemStockRepository;
            CheckConnection();
        }

        private bool CheckConnection() {
            return MqttConn.IsConnected
                || MqttConn.ConnectAsync().GetAwaiter().GetResult().ResultCode == MqttClientConnectResultCode.Success;

        }


        public bool Start(OrderPicking picking) {
            var stockitems = ItemStockRepository.Get(picking.Sector);

            var toNotify = picking.Items.GroupBy(g => g.SKU)
                .Select(item => new Tuple<PickingItem, ItemStock, int>(
                    item.First()
                    , stockitems.FirstOrDefault(f => f.SKU == item.First().SKU)
                    , item.Count()));

            if (toNotify.Any(x => x.Item2 == null))
                return false;

            bool success = toNotify
                .Select(s => StartOne(s.Item1, s.Item3, s.Item2))
                .Select(s => s.GetAwaiter().GetResult())
                .All(x => x.ReasonCode == MqttClientPublishReasonCode.Success);

            if (!success) {
                //Todo Cancel All
            }

            return success;
        }
        private async Task<MqttClientPublishResult> StartOne(PickingItem item, int qty, ItemStock stock) {
            PickingFaceMessage message = new StartMessage(stock.StockCode, item.Id,item.SKU,qty,item.Operator.Name,true,false);
            return await MqttConn.Publish(message.Topic, message.Message);
        }



        public void Finish(string sku, OrderPicking picking) {
            throw new NotImplementedException();
        }

        public void Approve(string sku, OrderPicking picking) {
            throw new NotImplementedException();
        }

        public void Reject(string sku, OrderPicking picking) {
            throw new NotImplementedException();
        }

        public void Cancel(OrderPicking picking) {
            throw new NotImplementedException();
        }

        public void Error(string sector) {
            throw new NotImplementedException();
        }


       

        

    }
}
