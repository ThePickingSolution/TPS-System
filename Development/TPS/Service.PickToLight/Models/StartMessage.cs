using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class StartMessage : PickingFaceMessage
    {
        private const string COMMAND = "PICK";
        private string ItemId;
        private string SKU;
        private int RedLED;
        private int GreenLED;
        private int Quantity;
        private string User;

        public StartMessage(string topic, string itemId, string sku,int qty, string user, bool redl, bool greenl) {
            Topic = topic;

            ItemId = itemId;
            SKU = sku;
            Quantity = qty;
            RedLED = redl ? 1 : 0;
            GreenLED = greenl ? 1 : 0;
            User = user.Length > 9 ? user.Substring(0, 9) : user;
        }
        protected override string GetMessage() {
            return $"{COMMAND};{ItemId};{GreenLED};{RedLED};{Quantity};{User};\0";
        }
    }
}
