using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    public interface ICountMST
    {
        double CountMST(Graph Gra, out List<Kante> kanten);
    }
}
