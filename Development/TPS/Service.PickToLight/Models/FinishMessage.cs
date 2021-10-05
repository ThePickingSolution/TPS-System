using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class FinishMessage : PickingFaceMessage
    {
        private const string COMMAND = "DONE";
        private string ItemId;

        public FinishMessage(string topic, string itemId) {
            Topic = topic;

            ItemId = itemId;
        }
        protected override string GetMessage() {
            return $"{COMMAND};{ItemId};\0";
        }
    }
}
