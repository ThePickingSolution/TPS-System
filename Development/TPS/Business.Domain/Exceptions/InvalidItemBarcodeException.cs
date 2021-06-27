using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Exceptions
{
    public class InvalidItemBarcodeException : DomainException
    {
        public InvalidItemBarcodeException() : base() { }

        public InvalidItemBarcodeException(string message) : base(message) { }

        public InvalidItemBarcodeException(string message, Exception innerException) : base(message, innerException) { }

    }
}
