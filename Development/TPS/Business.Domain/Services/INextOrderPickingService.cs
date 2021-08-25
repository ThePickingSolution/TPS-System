using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Services
{
    public interface INextOrderPickingService
    {
        string NextOrderPicking(string sector);
    }
}
