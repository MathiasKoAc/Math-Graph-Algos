using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    interface IParseGraph
    {
        Graph parseGraph(int count, string[] lines, bool debug);
    }
}
