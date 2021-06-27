using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class InvalidStatusChangeException : DomainException
    {
        public InvalidStatusChangeException() : base() { }

        public InvalidStatusChangeException(string message) : base(message) { }

        public InvalidStatusChangeException(string message, Exception innerException) : base(message, innerException) { }

    }
}
