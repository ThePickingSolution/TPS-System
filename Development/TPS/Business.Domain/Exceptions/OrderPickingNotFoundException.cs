using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class OrderPickingNotFoundException : DomainException
    {
        public OrderPickingNotFoundException() : base() { }

        public OrderPickingNotFoundException(string message) : base(message) { }

        public OrderPickingNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
