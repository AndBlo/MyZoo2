using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Exceptions
{
    class UnableToRemoveObjectException : ApplicationException
    {
        public UnableToRemoveObjectException(string message) : base(message)
        {
        }
    }
}
