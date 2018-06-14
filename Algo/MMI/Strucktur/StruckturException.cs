using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class StruckturException : Exception
    {
        public StruckturException(string message) : base(message)
        {
        }
    }
}
