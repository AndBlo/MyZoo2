using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Exceptions
{
    class RequiredFieldsNullException : ApplicationException
    {
        public RequiredFieldsNullException(string message) : base(message)
        {
        }
    }
}
