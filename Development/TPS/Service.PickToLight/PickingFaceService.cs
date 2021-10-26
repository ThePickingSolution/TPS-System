using Business.Domain.People;
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
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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


            Thread.Sleep(1000);
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
                .Select(s => {
                    return StartOne(s.Item1, s.Item3, s.Item2, picking.Operator).GetAwaiter().GetResult();
                })
                .All(x => x.ReasonCode == MqttClientPublishReasonCode.Success);

            if (!success) {
                this.Cancel(picking);
            }

            return success;
        }
        private async Task<MqttClientPublishResult> StartOne(PickingItem item, int qty, ItemStock stock, Operator op) {
            SetupSubscribeConfirm(stock);
            PickingFaceMessage message = new StartMessage($"{stock.StockCode}/sys", item.Id,item.SKU,qty, op.Name,true,false);
            return await MqttConn.Publish(message.Topic, message.Message);
        }

        public bool Approve(string sku, OrderPicking picking) {
            var stockitem = ItemStockRepository.Get(picking.Sector)
                   .FirstOrDefault(i => i.SKU == sku);
            if (stockitem == null)
                return false;

            var result = ApproveOne(picking.Items.First(i => i.SKU == sku), stockitem).GetAwaiter().GetResult().ReasonCode;
            return result == MqttClientPublishReasonCode.Success;
        }
        private async Task<MqttClientPublishResult> ApproveOne(PickingItem item, ItemStock stock) {
            UnsubscribeConfirm(stock);
            PickingFaceMessage message = new ApproveMessage($"{stock.StockCode}/sys", item.Id);
            return await MqttConn.Publish(message.Topic, message.Message);
        }
        
        public bool Reject(string sku, OrderPicking picking) {
            var stockitem = ItemStockRepository.Get(picking.Sector)
                   .FirstOrDefault(i => i.SKU == sku);
            if (stockitem == null)
                return false;

            var result = RejectOne(picking.Items.First(i => i.SKU == sku), stockitem).GetAwaiter().GetResult().ReasonCode;
            return result == MqttClientPublishReasonCode.Success;
        }
        private async Task<MqttClientPublishResult> RejectOne(PickingItem item, ItemStock stock) {
            PickingFaceMessage message = new RejectMessage($"{stock.StockCode}/sys", item.Id);
            return await MqttConn.Publish(message.Topic, message.Message);
        }


        public bool Finish(string sku, OrderPicking picking) {

            var stockitem = ItemStockRepository.Get(picking.Sector)
                   .FirstOrDefault(i => i.SKU == sku);
            if (stockitem == null)
                return false;

            var result = FinishOne(picking.Items.First(i => i.SKU == sku), stockitem).GetAwaiter().GetResult().ReasonCode;
            return result == MqttClientPublishReasonCode.Success;
        }
        private async Task<MqttClientPublishResult> FinishOne(PickingItem item, ItemStock stock) {
            UnsubscribeConfirm(stock);
            PickingFaceMessage message = new FinishMessage($"{stock.StockCode}/sys", item.Id);
            return await MqttConn.Publish(message.Topic, message.Message);
        }


        public bool Cancel(OrderPicking picking) {
            var stockitems = ItemStockRepository.Get(picking.Sector);

            var toNotify = picking.Items.GroupBy(g => g.SKU)
                .Select(item => new Tuple<PickingItem, ItemStock>(
                    item.First()
                    , stockitems.FirstOrDefault(f => f.SKU == item.First().SKU)));

            if (toNotify.Any(x => x.Item2 == null))
                return false;

            bool success = toNotify
                .Select(s => CancelOne(s.Item1, s.Item2).GetAwaiter().GetResult())
                .All(x => x.ReasonCode == MqttClientPublishReasonCode.Success);

            return success;
        }
        private async Task<MqttClientPublishResult> CancelOne(PickingItem item, ItemStock stock) {
            UnsubscribeConfirm(stock);
            PickingFaceMessage message = new CancelMessage($"{stock.StockCode}/sys", item.Id);
            return await MqttConn.Publish(message.Topic, message.Message);
        }


        public bool Error(string sector,string reason) {
            var stockitems = ItemStockRepository.Get(sector);

            return stockitems
                .Select(s => ErrorOne(s, reason).GetAwaiter().GetResult())
                .All(x => x.ReasonCode == MqttClientPublishReasonCode.Success);
        }
        private async Task<MqttClientPublishResult> ErrorOne(ItemStock stock,string error) {
            PickingFaceMessage message = new ErrorMessage($"{stock.StockCode}/sys", error);
            return await MqttConn.Publish(message.Topic, message.Message);
        }



        private bool SetupSubscribeConfirm(ItemStock stock) {
            string topic = $"{stock.StockCode}/esp";
            bool success = MqttConn.Subscribe(topic)
                .GetAwaiter()
                .GetResult() != null;
            if (!success) {
                Debug.WriteLine($"MQTT - Erro ao subscrever a confirmação para {stock.StockCode}");
            }
            return success;
        }
        private bool UnsubscribeConfirm(ItemStock stock) {
            string topic = $"{stock.StockCode}/esp";
            bool success = MqttConn.UnSubscribe(topic)
                .GetAwaiter()
                .GetResult() != null;
            if (!success) {
                Debug.WriteLine($"MQTT - Erro ao dessubscrever a confirmação para {stock.StockCode}");
            }
            return success;
        }
    }
}
