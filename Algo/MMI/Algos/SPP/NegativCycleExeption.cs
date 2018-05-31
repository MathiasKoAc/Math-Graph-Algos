using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class NegativCycleExeption : Exception
    {
        private string v;

        public NegativCycleExeption(string v)
        {
            this.v = v;
        }
    }
}
