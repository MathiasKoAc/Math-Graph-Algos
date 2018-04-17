using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    public interface ICountTSP
    {
        double roundTripp(Graph g, Knoten startKnoten, out List<Knoten> tour);
    }
}
