using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Exceptions
{
    public class AdministrationException : Exception
    {
        public AdministrationException() : base() { }

        public AdministrationException(string message) : base(message) { }

        public AdministrationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
