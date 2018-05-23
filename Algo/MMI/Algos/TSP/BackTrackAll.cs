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
        private List<Kante> bestKantenList;
        private HashSet<Knoten> bestKnotenList;
        private double bestGesamtGewicht;
        private Knoten _startKnoten;

        public double roundTripp(Graph g, Knoten startKnoten, out List<Knoten> tour)
        {
            double bestGesamt = allRoundTripps(g, startKnoten, out List<List<Kante>> touren, out List<Kante> bestTour, true);
            tour = bestKnotenList.ToList<Knoten>();
            tour.Add(tour[0]);
            return bestGesamt;
        }


        public double allRoundTripps(Graph g, Knoten startKnoten, out List<List<Kante>> touren, out List<Kante> bestTour, bool branchAndBound = false)
        {
            this.graph = g;
            this.bestKantenList = null;
            this.kantenListListe = new List<List<Kante>>();
            this.bestGesamtGewicht = Double.MaxValue;
            this._startKnoten = startKnoten;

            HashSet<Knoten> knotenCheck = new HashSet<Knoten>();
            knotenCheck.Add(startKnoten);

            foreach (Kante kant in startKnoten.Kanten)
            {
                deep(kant, new List<Kante>(), new HashSet<Knoten>(knotenCheck), 0d, branchAndBound);
            }
            touren = kantenListListe;
            bestTour = bestKantenList;
            return bestGesamtGewicht;
        }
        
        private void deep(Kante startKant, List<Kante> kanten, HashSet<Knoten> knoten, double gesamtGewicht, bool branchAndBound)
        {
            Knoten startKn = startKant.ToKnoten;
            knoten.Add(startKn);
            kanten.Add(startKant);
            gesamtGewicht += startKant.Gewicht;

            if (knoten.Count == graph.Knoten.Count) //Letzter Knoten / Letzte Kante
            {
                setLetzteKante(ref startKant, ref kanten, ref knoten, ref gesamtGewicht);
                
            } else
            {
                Knoten toKn;
                foreach (Kante kant in startKn.Kanten)
                {
                    toKn = kant.ToKnoten;
                    if (!knoten.Contains(toKn) && (!branchAndBound || bestGesamtGewicht > (gesamtGewicht + kant.Gewicht)))
                    {
                        //neuer Knoten und ist potentiell kuertzer als BesteLoesung bis hier
                        deep(kant, new List<Kante>(kanten), new HashSet<Knoten>(knoten), gesamtGewicht, branchAndBound);
                    }
                }
            }
        }

        private void setLetzteKante(ref Kante startKant, ref List<Kante> kanten, ref HashSet<Knoten> knoten, ref double gesamtGewicht)
        {
            Kante letzteKante = graph.findKante(startKant.ToKnoten, _startKnoten);
            if (letzteKante != null)
            {
                kanten.Add(letzteKante);
                gesamtGewicht += letzteKante.Gewicht;
                this.kantenListListe.Add(kanten);
                this.setIfBest(kanten, knoten, gesamtGewicht);
            }
    }

        private void setIfBest(List<Kante> kantenList, HashSet<Knoten> knotenList, double gesammt)
        {
            if(gesammt <  this.bestGesamtGewicht)
            {
                bestGesamtGewicht = gesammt;
                bestKantenList = kantenList;
                bestKnotenList = knotenList;
            }
        }
    }
}
