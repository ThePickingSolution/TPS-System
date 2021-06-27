using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    class InvalidSKUException : DomainException
    {
        public InvalidSKUException() : base() { }

        public InvalidSKUException(string message) : base(message) { }

        public InvalidSKUException(string message, Exception innerException) : base(message, innerException) { }

    }
}
