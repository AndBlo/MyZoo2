using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Exceptions
{
    class InvalidBookingDateTimeException : ApplicationException
    {
        public InvalidBookingDateTimeException(string message) : base(message)
        {
        }
    }
}
