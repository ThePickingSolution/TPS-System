using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Exceptions
{
     public class UsernameNotUniqueException : AdministrationException
    {
        public UsernameNotUniqueException() : base() { }

        public UsernameNotUniqueException(string message) : base(message) { }

        public UsernameNotUniqueException(string message, Exception innerException) : base(message, innerException) { }
    }
}
