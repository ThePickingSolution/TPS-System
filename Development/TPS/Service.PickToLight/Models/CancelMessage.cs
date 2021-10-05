using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class CancelMessage : PickingFaceMessage
    {
        private const string COMMAND = "CANCEL";
        private string ItemId;

        public CancelMessage(string topic, string itemId) {
            Topic = topic;

            ItemId = itemId;
        }
        protected override string GetMessage() {
            return $"{COMMAND};{ItemId};\0";
        }
    }
}
