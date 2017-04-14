using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Graph
    {
        private List<Kante> kanten;
        private Dictionary<int, Knoten> knoten;

        public Graph (List<Kante> Kanten, Dictionary<int, Knoten> Knoten)
        {
            kanten = Kanten;
            knoten = Knoten;
        }
    }
}
