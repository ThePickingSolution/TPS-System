using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Events
{
    public interface IPickingEvent<T> where T : class
    {
        void SetThisEventTo(T model);
    }
}
