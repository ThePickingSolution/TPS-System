using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Exceptions
{
    public class RegistryNotFoundException : AdministrationException
    {
        public RegistryNotFoundException() : base() { }

        public RegistryNotFoundException(string message) : base(message) { }

        public RegistryNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
