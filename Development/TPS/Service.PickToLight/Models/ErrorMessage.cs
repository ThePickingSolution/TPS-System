using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal class ErrorMessage : PickingFaceMessage
    {
        private const string COMMAND = "ERROR";
        private string Error;

        public ErrorMessage(string topic, string error) {
            Topic = topic;
            Error = error;
        }
        protected override string GetMessage() {
            return $"{COMMAND};{Error};\0";
        }
    }
}
