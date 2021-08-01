using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Exceptions
{
    public class AdmNullValidatorException : AdministrationException
    {
        public AdmNullValidatorException() : base() { }

        public AdmNullValidatorException(string message) : base(message) { }

        public AdmNullValidatorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
