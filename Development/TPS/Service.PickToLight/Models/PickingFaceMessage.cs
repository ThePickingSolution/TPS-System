using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Models
{
    internal abstract class PickingFaceMessage
    {
        public string Topic { get; protected set; }
        public string Message { get => GetMessage(); }


        protected abstract string GetMessage();
    }
}
