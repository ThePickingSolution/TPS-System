using Application.Picking.Interface.DTOs;
using Business.Domain.Picking;
using Infrastructure.MQTT;
using Service.PickToLight.Interface;
using Service.PickToLight.Models;
using Service.PickToLight.Picking;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight
{
    public interface IConfirmListenerService
    {
        void Setup();
    }


    public class ConfirmListenerService : IConfirmListenerService
    {
        private readonly MQTTConnection mqttConnection;
        private readonly IOrderPickingProxyRepository orderPickingProxy;
        private readonly IPickingItemProcessProxyRepository pickingItemProxy;

        public ConfirmListenerService(
            MQTTConnection _mqttConn,
            IOrderPickingProxyRepository _orderPickingProxy,
            IPickingItemProcessProxyRepository _pickingItemProxy
            ) {

            mqttConnection = _mqttConn;
            pickingItemProxy = _pickingItemProxy;
            orderPickingProxy = _orderPickingProxy;
        }

        public void Setup() {
            mqttConnection.SetListener((args) => {
                var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);
                var message = new ConfirmMessage(payload);
                if (message.IsMessage) {
                    PickComponents(message.ItemID, message.Quantity,args.ApplicationMessage.Topic.Replace("/esp",""));
                }
            });
        }

        public void PickComponents(string itemid, int qty, string stockCode) {
            var orderpicking = orderPickingProxy.FindOfItem(itemid);
            if (orderpicking.Status == PickingStatus.WIP) {
                var item = orderpicking.Items.First(f => f.Id == itemid);

                var sameSkuItems = orderpicking.Items.Where(w => w.SKU == item.SKU);
                var count = qty;


                bool success = sameSkuItems.All(i => pickingItemProxy.PickItem(new PickingItemDto() {
                    Id = i.Id,
                    Status = --count < 0 ? ItemStatus.MISSING : ItemStatus.PICKED,
                    Operator = orderpicking.Operator
                }));

                if (success) {
                    var message = new ApproveMessage($"{stockCode}/sys", itemid);
                    mqttConnection.Publish(message.Topic, message.Message).GetAwaiter();
                } else {
                    var message = new RejectMessage($"{stockCode}/sys", itemid);
                    mqttConnection.Publish(message.Topic, message.Message).GetAwaiter();
                }
            }
        }
    }
}
