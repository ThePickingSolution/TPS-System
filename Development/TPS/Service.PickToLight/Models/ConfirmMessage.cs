using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class ConfirmMessage
    {
        private const string CODE = "CONFIRM";
        
        public bool IsMessage { get; private set; }
        public string ItemID { get; private set; }
        public int Quantity { get; private set; }

        public ConfirmMessage(string payload) {
            IsMessage = payload.Split(';')[0].ToUpper().Equals(CODE);
            if (IsMessage) {
                ItemID = payload.Split(';')[1];
                Quantity = Int32.Parse(payload.Split(';')[2]);
            }
        }
    }
}
