using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyZoo.Exceptions
{
    class DbObjectNotFoundException : ApplicationException
    {
        public DbObjectNotFoundException(string message) : base(message)
        {
        }
    }
}
