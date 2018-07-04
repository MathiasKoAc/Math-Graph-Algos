using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class NotBflussException : Exception
    {
        public NotBflussException(string message) : base(message)
        {
        }
    }
}
