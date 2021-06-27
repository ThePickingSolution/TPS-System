using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Picking
{
    public enum PickingStatus
    {
        PENDING = 1000,
        WIP = 2000,
        READY = 3000,
        PICKED = 4000
    }
}
