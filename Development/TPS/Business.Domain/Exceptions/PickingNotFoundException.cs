using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class PickingNotFoundException : DomainException
    {
        public PickingNotFoundException() : base() { }

        public PickingNotFoundException(string message) : base(message) { }

        public PickingNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
