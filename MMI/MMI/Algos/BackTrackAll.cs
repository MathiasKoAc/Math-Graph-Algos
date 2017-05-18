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

        public void allRoundTripps(Graph g, Knoten startKnoten, out List<List<Kante>> touren)
        {
            this.graph = g;
            this.bestkantenList = null;
            this.kantenListListe = new List<List<Kante>>();
            this.bestGesamtGewicht = 0d;

            foreach(Kante kant in startKnoten.Kanten)
            {
                deep(kant, new List<Kante>(), new HashSet<int>(), 0d);
            }
            touren = kantenListListe;
        }
        
        private void deep(Kante startKant, List<Kante> kanten, HashSet<int> knoten, double gesamtGewicht)
        {
            Knoten startKn = startKant.ToKnoten;
            knoten.Add(startKn.Wert);
            kanten.Add(startKant);
            gesamtGewicht += startKant.Gewicht;

            if (knoten.Count == graph.Knoten.Count)
            {
                //letzter Knoten
                this.kantenListListe.Add(kanten);
            } else
            {
                Knoten toKn;
                foreach (Kante kant in startKn.Kanten)
                {
                    toKn = kant.ToKnoten;
                    if (!knoten.Contains(toKn.Wert))
                    {
                        //neuer Knoten
                        deep(kant, new List<Kante>(kanten), new HashSet<int>(knoten), gesamtGewicht);
                    }
                }
            }
        }
    }
}
