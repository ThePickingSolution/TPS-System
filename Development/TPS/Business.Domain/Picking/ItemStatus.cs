using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Picking
{
    public enum ItemStatus
    {
        PENDING = 1000,
        PENDING_READING = 1500,
        NO_READING = 2000,
        MISSING = 3000,
        PICKED = 4000
    }
}
