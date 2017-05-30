using System;
using System.Collections.Generic;


namespace MMI.Algos
{
    public class Dijkstra
    {
        public void sortestWay(Graph gra, Knoten Startknoten)
        {
            List<Knoten> sortList = new List<Knoten>();
            initialisiere(ref gra, ref Startknoten, ref sortList);
            Knoten nextKnoten;
            //<Vorgaenger, Nachfolger>
            Dictionary<Knoten, Knoten> vorgaengerDict = new Dictionary<Knoten, Knoten>();
            while (sortList.Count > 0) {
                nextKnoten = pullWithShortesDistanz(ref sortList);
                updateDistanz(nextKnoten, ref vorgaengerDict);
            }
        }

        private void initialisiere(ref Graph g,  ref Knoten Startknoten, ref List<Knoten> List)
        {
            foreach(KeyValuePair<int, Knoten> pair in g.Knoten)
            {
                if(pair.Value == Startknoten)
                {
                    pair.Value.Distance = 0;
                    pair.Value.Vorgaenger = pair.Value;
                } else
                {
                    pair.Value.Distance = Double.MaxValue;
                    pair.Value.Vorgaenger = null;

                }
                List.Add(pair.Value);
            } 
        }

        private void updateDistanz(Knoten k, ref Dictionary<Knoten, Knoten> vorgaengerList)
        {
            double neuDistance = 0d;
            Knoten nachfolger;
            foreach(Kante kant in k.Kanten)
            {
                neuDistance = k.Distance + kant.Gewicht;
                if(kant.ToKnoten.Distance > neuDistance)
                {

                    if (vorgaengerList.TryGetValue(kant.ToKnoten.Vorgaenger, out nachfolger))
                    {
                        vorgaengerList.Remove(kant.ToKnoten.Vorgaenger);
                    }
                    vorgaengerList.Add(k, kant.ToKnoten);
                    kant.ToKnoten.Distance = neuDistance;
                    kant.ToKnoten.Vorgaenger = k;
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
                        best.Distance = knot.Distance;
                    }
                }

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
