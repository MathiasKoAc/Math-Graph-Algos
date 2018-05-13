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

            //Dreiecksungleichung: den Rückweg oder die direkte Kante
            if(lastStartKant != null && lastStartKant.Gewicht < wert)
            {
                wert += lastStartKant.Gewicht;
                Console.WriteLine("Z: " + lastStartKant.ToString());
            }
            else
            {
                wert =+ wert;
            }

            return wert;
        }

        private Kante findNextKante(Knoten startKnoten)
        {
            Kante bestKante = null;
            foreach (Kante kant in startKnoten.Kanten)
            {
                if (kant.ToKnoten.Tag == -1 && (bestKante == null || bestKante.Gewicht > kant.Gewicht))
                {
                    bestKante = kant;
                }
            }
            return bestKante;
        }
    }
}
