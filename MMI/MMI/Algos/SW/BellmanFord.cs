using System;
using System.Collections.Generic;


namespace MMI.Algos
{
    class BellmanFord
    {
        public bool sortestWay(ref Graph gra, Knoten Startknoten)
        {
            List<Knoten> sortList = new List<Knoten>();
            initialisiere(ref gra, ref Startknoten, ref sortList);

            for(int i = 0; i < sortList.Count; i++)
            {
                foreach(Kante kant in gra.Kanten)
                {
                    if(kant.FromKnoten.Distance + kant.Gewicht < kant.ToKnoten.Distance)
                    {
                        kant.ToKnoten.Distance = kant.FromKnoten.Distance + kant.Gewicht;
                        kant.ToKnoten.Vorgaenger = kant.FromKnoten;
                        kant.ToKnoten.VorgaengerKante = kant;
                    }
                }
            }

            return checkUpNegativerZyklus(ref gra);
        }

        private bool checkUpNegativerZyklus(ref Graph gra)
        {
            foreach (Kante kant in gra.Kanten)
            {
                if (kant.FromKnoten.Distance + kant.Gewicht < kant.ToKnoten.Distance)
                {
                    Console.WriteLine("Negativer Zyklus");
                    return false;
                }
            }
            return true;
        }

        private void initialisiere(ref Graph g, ref Knoten Startknoten, ref List<Knoten> List)
        {
            foreach (KeyValuePair<int, Knoten> pair in g.Knoten)
            {
                if (pair.Value.Wert == Startknoten.Wert)
                {
                    pair.Value.Distance = 0;
                    pair.Value.Vorgaenger = pair.Value;
                }
                else
                {
                    pair.Value.Distance = Double.MaxValue;
                    pair.Value.Vorgaenger = null;

                }
                List.Add(pair.Value);
            }
        }
    }
}
