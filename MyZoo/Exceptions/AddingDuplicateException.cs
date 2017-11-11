using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Exceptions
{
    class AddingDuplicateException : ApplicationException
    {
        public AddingDuplicateException(string message) : base(message)
        {
        }
    }
}
