using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class InvalidOrderPickingIdException : DomainException
    {
        public InvalidOrderPickingIdException() : base() { }

        public InvalidOrderPickingIdException(string message) : base(message) { }

        public InvalidOrderPickingIdException(string message, Exception innerException) : base(message, innerException) { }

    }
}
