using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class NullOperatorException : DomainException
    {
        public NullOperatorException() : base() { }

        public NullOperatorException(string message) : base(message) { }

        public NullOperatorException(string message, Exception innerException) : base(message, innerException) { }

    }
}
