using Microsoft.Extensions.DependencyInjection;
using Picking.Hardware.Handler.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace API.Picking
{
    public class HarwareStartup
    {
        public HarwareStartup() { }

        public void Start(IServiceProvider provider) {

            IHardwareHandlerManager manager = (IHardwareHandlerManager)provider.GetService(typeof(IHardwareHandlerManager));

            Debug.WriteLine("Starting Harware Manager");
            Debug.WriteLine(manager.Start() ? "Harware Started" : "Hardware Manager failed to start");
        }
    }
}
