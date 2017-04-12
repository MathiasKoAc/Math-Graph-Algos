using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    interface IParseGraph
    {
        Graph parseGraph(string[] lines, bool debug);
    }
}
