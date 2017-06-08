using System;
using System.Collections.Generic;


namespace MMI.Algos
{
    public class Dijkstra
    {

        public void sortestWay(ref Graph gra, Knoten Startknoten)
        {
            List<Knoten> sortList = new List<Knoten>();
            initialisiere(ref gra, ref Startknoten, ref sortList);
            Knoten nextKnoten = null;

            while (sortList.Count > 0) {
                nextKnoten = pullWithShortesDistanz(ref sortList);
                updateDistanz(ref nextKnoten);
            }
        }

        private void initialisiere(ref Graph g,  ref Knoten Startknoten, ref List<Knoten> List)
        {
            foreach(KeyValuePair<int, Knoten> pair in g.Knoten)
            {
                if(pair.Value.Wert == Startknoten.Wert)
                {
                    pair.Value.Distance = 0;
                    pair.Value.Vorgaenger = pair.Value;
                } else
                {
                    pair.Value.Distance = 100000d;
                    //pair.Value.Distance = Double.MaxValue;
                    pair.Value.Vorgaenger = null;

                }
                List.Add(pair.Value);
            } 
        }

        private void updateDistanz(ref Knoten fokusKnoten)
        {
            double neuDistance = 0d;
            foreach(Kante kant in fokusKnoten.Kanten)
            {
                neuDistance = fokusKnoten.Distance + kant.Gewicht;
                if(kant.ToKnoten.Distance > neuDistance)
                {
                    kant.ToKnoten.Distance = neuDistance;
                    kant.ToKnoten.Vorgaenger = fokusKnoten;
                }
            }
        }

        private Knoten pullWithShortesDistanz(ref List<Knoten> knotens)
        {
            if(knotens != null)
            {
                Knoten best = knotens[0];
                foreach (Knoten knot in knotens)
                {
                    if(knot.Distance < best.Distance)
                    {
                        best = knot;
                    }
                }

                Console.WriteLine("Konten: " + best.Wert + "Dist: " + best.Distance);

                knotens.Remove(best);
                return best;
            }
            else
            {
                return null;
            }
            
        }
    }
}
