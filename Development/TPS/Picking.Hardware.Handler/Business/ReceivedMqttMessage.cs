using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.Business
{
    public class ReceivedMqttMessage
    {
        public string Message { get; private set; }

        public bool IsReceived { get { return Message.Split(';')[0].Trim().Equals("RECEIVED"); } }
        public bool IsConfirm { get { return Message.Split(';')[0].Trim().Equals("DONE"); } }

        public string ItemId {
            get {
                return Message.Split(';')[1].Trim();
            }
        }
        public int MessageNumber {
            get {
                return Int32.Parse(Message.Split(';')[2].Trim());
            }
        }

        public int Quantity {
            get {
                return IsConfirm ? Int32.Parse(Message.Split(';')[3].Trim()) : 0;
            }
        }

        public ReceivedMqttMessage(string message) {
            this.Message = message;
        }
    }
}
