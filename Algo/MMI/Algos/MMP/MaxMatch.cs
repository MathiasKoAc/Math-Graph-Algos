using System;
using System.Collections.Generic;
using System.Linq;

namespace MMI.Algos
{
    class MaxMatch
    {
        public double calcMaxMatchingRelativ(Graph g, out List<Kante> matchKanten)
        {
            double anzMatchKnoten = this.calcMaxMatching(g, out matchKanten);
            return anzMatchKnoten / ((double)g.getAnzKnoten());
        }

        public int calcMaxMatching(Graph g, out List<Kante> matchKanten)
        {
            SortedSet<Knoten> quellenList = new SortedSet<Knoten>();
            SortedSet<Knoten> senkenList = new SortedSet<Knoten>();

            foreach (Kante kant in g.Kanten) {
                quellenList.Add(kant.FromKnoten);
                senkenList.Add(kant.ToKnoten);
                kant.Fluss = 0;
                kant.Kapazitaet = 1;
                kant.FromKnoten.Balance = 1;
                kant.ToKnoten.Balance = -1;
            }

            g.setSuperQuelleSenke(quellenList.ToList<Knoten>(), senkenList.ToList<Knoten>(), out Knoten superQuelle, out Knoten superSenke, true);

            int anzahlMatches = (int) (new EdmondsKarp().calcMFP(ref g, superQuelle, superSenke));

            matchKanten = g.Kanten.Where(kant => kant.Fluss == 1).ToList<Kante>();

            return anzahlMatches;
        }
    }
}