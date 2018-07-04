using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CycleCancelling
    {
        public double calcKMF(Graph g)
        {
            double initalKosten = calcInitalBfluss(ref g, out Knoten superQuelle);

            Graph resiGra = g.createResidualGraph();
            findNegativCycle(ref resiGra, ref superQuelle);

            return 0d;
        }

        //true wenn Cycle gefunden
        private bool findNegativCycle(ref Graph resiGra, ref Knoten superQuelle)
        {
            bool zyklus = ! (new MoorBellmanFord().ShortestWayTree(resiGra, superQuelle, out List<DijKnoten> wayTree));

            if(zyklus)
            {

            }

            return zyklus;
        }

        private double calcInitalBfluss(ref Graph g, out Knoten superQuelle)
        {
            List<Knoten> senken = new List<Knoten>();
            List<Knoten> quellen = new List<Knoten>();

            foreach(Knoten k in g.Knoten)
            {
                if(k.Balance > 0)
                {
                    quellen.Add(k);
                } else if(k.Balance < 0)
                {
                    senken.Add(k);
                }
            }

            g.setSuperQuelleSenke(quellen, senken, out superQuelle, out Knoten superSenke, true);
            new EdmondsKarp().calcMFP(g, superQuelle, superSenke);

            g.delSuperSenke(senken, superSenke);

            return 0d;
            
        }
    }
}
