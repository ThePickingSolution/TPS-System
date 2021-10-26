using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PickToLight.Interface
{
    public interface IConfirmPickingService
    {
        void PickComponents(string itemid, int qty);
    }
}
