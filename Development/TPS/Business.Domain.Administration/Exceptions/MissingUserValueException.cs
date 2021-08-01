using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Exceptions
{
    public class MissingUserValueException : AdministrationException
    {
        public MissingUserValueException() : base() { }

        public MissingUserValueException(string message) : base(message) { }

        public MissingUserValueException(string message, Exception innerException) : base(message, innerException) { }
    }
}
