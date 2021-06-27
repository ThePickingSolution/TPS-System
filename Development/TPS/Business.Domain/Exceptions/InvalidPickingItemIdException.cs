using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class InvalidPickingItemIdException : DomainException
    {
        public InvalidPickingItemIdException() : base() { }

        public InvalidPickingItemIdException(string message) : base(message) { }

        public InvalidPickingItemIdException(string message, Exception innerException) : base(message, innerException) { }

    }
}
