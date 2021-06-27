using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class InvalidContainerException : DomainException
    {
        public InvalidContainerException() : base() { }

        public InvalidContainerException(string message): base(message) { }

        public InvalidContainerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
