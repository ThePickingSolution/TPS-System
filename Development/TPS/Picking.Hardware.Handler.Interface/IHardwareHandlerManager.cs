using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Picking.Hardware.Handler.Interface
{
    public interface IHardwareHandlerManager
    {
        bool Start();
        bool Stop();
    }
}
