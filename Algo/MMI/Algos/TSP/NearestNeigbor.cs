using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class NearestNeigbor : ICountTSP
    {
        public double roundTripp(Graph g, Knoten startKnoten,  out List<Knoten> tour)
        {
            var wert = 0d;
            tour = new List<Knoten>();

            Knoten nextKnoten = startKnoten;
            Knoten lastKnoten = null;
            nextKnoten.Tag = 1;
            tour.Add(nextKnoten);

            Kante bestKante = null;
            
            while (nextKnoten != null)
            {
                bestKante = findNextKante(nextKnoten);
                lastKnoten = nextKnoten;
                nextKnoten = null;
                if(bestKante != null)
                {
                    nextKnoten = bestKante.ToKnoten;
                    nextKnoten.Tag = 1;
                    tour.Add(nextKnoten);
                    Console.WriteLine("Z: " + bestKante.ToString());
                    wert += bestKante.Gewicht;
                }
            }

            tour.Add(startKnoten);
            Kante lastStartKant = g.findKante(lastKnoten, startKnoten);

            wert += lastStartKant.Gewicht;
            Console.WriteLine("Z: " + lastStartKant.ToString());

            return wert;
        }

        private Kante findNextKante(Knoten startKnoten)
        {
            Kante bestKante = null;
            double bestGewicht = Double.MaxValue;
            foreach (Kante kant in startKnoten.Kanten)
            {
                if (kant.ToKnoten.Tag == -1 && bestGewicht > kant.Gewicht)
                {
                    bestKante = kant;
                    bestGewicht = kant.Gewicht;
                }
            }
            return bestKante;
        }
    }
}
