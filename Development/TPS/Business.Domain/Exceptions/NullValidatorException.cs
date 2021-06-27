using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    class NullValidatorException : DomainException
    {
        public NullValidatorException() : base() { }

        public NullValidatorException(string message) : base(message) { }

        public NullValidatorException(string message, Exception innerException) : base(message, innerException) { }

    }
}
