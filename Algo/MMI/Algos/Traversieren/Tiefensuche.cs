using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos.Traversieren
{
    class Tiefensuche
    {
        private List<Knoten> knotenList;

        public List<Knoten> durchlaufen(Knoten startknoten)
        {
            knotenList = new List<Knoten>();
            deep(startknoten);
            return knotenList;
        }

        private void deep(Knoten startKnoten)
        {
            startKnoten.Tag = 1;
            knotenList.Add(startKnoten);
            Knoten toKn;
            foreach (Kante kant in startKnoten.Kanten)
            {
                toKn = kant.ToKnoten;
                if (toKn.Tag == -1)
                {
                    //Tag -1 also neuer Knoten
                    deep(kant.ToKnoten);
                }
            }
        }
    }
}
