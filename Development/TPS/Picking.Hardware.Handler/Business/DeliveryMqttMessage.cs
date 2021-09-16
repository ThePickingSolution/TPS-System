using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.Business
{
    public class DeliveryMqttMessage
    {
        private string ItemId;
        private string ItemReference;
        private int Quantity;
        private string User;

        private int MessageNumber;

        public DeliveryMqttMessage(List<PickingItem> items, string user) {
            ItemId = items[0].Id;
            ItemReference = items[0].SKU;
            Quantity = items.Count;
            User = user;
        }
        public DeliveryMqttMessage(PickingItem item, string user) {
            ItemId = item.Id;
            ItemReference = item.SKU;
            Quantity = 1;
            User = user;
        }

        public string PickMessage(int messageNumber) {
            this.MessageNumber = messageNumber;
            return $"PICK;{ItemId};{messageNumber};1;0;{ItemReference};{Quantity};{User}";
        }
        public string FinishMessage(int messageNumber) {
            this.MessageNumber = messageNumber;
            return $"DONE;{ItemId};{messageNumber}";
        }
        public string ReceicedMessage(int messageNumber) {
            this.MessageNumber = messageNumber;
            return $"RECEIVED;{ItemId};{messageNumber}";
        }

        public static List<DeliveryMqttMessage> Create(OrderPicking orderPicking) {
            return orderPicking.Items.GroupBy(g => g.SKU)
                .Select(group => new DeliveryMqttMessage(group.ToList(), orderPicking.Operator.Username))
                .ToList();
        }
    }
}
