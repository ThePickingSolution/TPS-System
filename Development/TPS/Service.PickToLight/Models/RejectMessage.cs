using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class RejectMessage : PickingFaceMessage
    {
        private const string COMMAND = "REJECTED";
        private string ItemId;

        public RejectMessage(string topic, string itemId) {
            Topic = topic;

            ItemId = itemId;
        }
        protected override string GetMessage() {
            return $"{COMMAND};{ItemId};\0";
        }
    }
}
