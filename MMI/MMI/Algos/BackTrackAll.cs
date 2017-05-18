using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class BackTrackAll
    {
        private Graph graph;
        private List<List<Kante>> kantenListListe;
        private List<Kante> bestkantenList;
        private double bestGesamtGewicht;

        public double allRoundTripps(Graph g, Knoten startKnoten, out List<List<Kante>> touren, out List<Kante> bestTour, bool branchAndBound = false)
        {
            this.graph = g;
            this.bestkantenList = null;
            this.kantenListListe = new List<List<Kante>>();
            this.bestGesamtGewicht = Double.MaxValue;

            foreach(Kante kant in startKnoten.Kanten)
            {
                deep(kant, new List<Kante>(), new HashSet<byte>(), 0d, branchAndBound);
            }
            touren = kantenListListe;
            bestTour = bestkantenList;
            return bestGesamtGewicht;
        }
        
        private void deep(Kante startKant, List<Kante> kanten, HashSet<byte> knoten, double gesamtGewicht, bool branchAndBound)
        {
            Knoten startKn = startKant.ToKnoten;
            knoten.Add((byte)startKn.Wert);
            kanten.Add(startKant);
            gesamtGewicht += startKant.Gewicht;

            if (branchAndBound && this.bestGesamtGewicht < gesamtGewicht)
            {
                return;
            }

            if (knoten.Count == graph.Knoten.Count)
            {
                //letzter Knoten
                this.kantenListListe.Add(kanten);
                this.setIfBest(kanten, gesamtGewicht);

            } else
            {
                Knoten toKn;
                foreach (Kante kant in startKn.Kanten)
                {
                    toKn = kant.ToKnoten;
                    if (!knoten.Contains((byte)toKn.Wert))
                    {
                        //neuer Knoten
                        deep(kant, new List<Kante>(kanten), new HashSet<byte>(knoten), gesamtGewicht, branchAndBound);
                    }
                }
            }
        }

        private void setIfBest(List<Kante> kantenList, double gesammt)
        {
            if(gesammt <  this.bestGesamtGewicht)
            {
                bestGesamtGewicht = gesammt;
                bestkantenList = kantenList;
            }
        }
    }
}
