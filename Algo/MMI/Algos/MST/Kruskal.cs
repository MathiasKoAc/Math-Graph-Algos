using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Kruskal : ICountMST
    {
        public double CountMST(Graph Gra, out List<Kante> Kanten)
        {
            Gra.Kanten.Sort();
            Gra.resetKantenTag();
            Gra.resetKnotenTag();

            int maxTag = 0;
            Kanten = new List<Kante>();
            double mstSize = 0;

            foreach(Kante k in Gra.Kanten)
            {
                mstSize += addKante(k, ref Kanten, ref Gra, ref maxTag);
            }
            return mstSize;
        }

        protected double addKante(Kante k, ref List<Kante> Kanten, ref Graph Gra, ref int maxTag)
        {

            double added = 0.0;
            Knoten toKnoten = k.ToKnoten;
            Knoten fromKnoten = k.FromKnoten;


            //neue lose Kante
            if (toKnoten.Tag == -1 && fromKnoten.Tag == -1)
            {
                maxTag++;
                toKnoten.Tag = maxTag;
                fromKnoten.Tag = maxTag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if (toKnoten.Tag == -1 && fromKnoten.Tag >= 0)   //neue Kante toKnoten neu
            {
                toKnoten.Tag = fromKnoten.Tag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if (fromKnoten.Tag == -1 && toKnoten.Tag >= 0)   //neue Kante FromKonten neu
            {
                fromKnoten.Tag = toKnoten.Tag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if (toKnoten.Tag > fromKnoten.Tag)     //fromKnoten alter Graph
            {
                // andere alle Knoten mit dem toTag auf das Tag vom fromKnoten
                Kanten.Add(k);
                changeKontenTag(ref Gra.Knoten, toKnoten.Tag, fromKnoten.Tag, (Kanten.Count +1));
                added = k.Gewicht;
            }
            else if (toKnoten.Tag < fromKnoten.Tag)     //toKnoten alter Graph
            {
                // andere alle Knoten mit dem fromTag auf das Tag vom toKnoten
                Kanten.Add(k);
                changeKontenTag(ref Gra.Knoten, fromKnoten.Tag, toKnoten.Tag, (Kanten.Count + 1));
                added = k.Gewicht;
            }
            return added;
        }

        protected void changeKontenTag(ref List<Knoten> knotenList, int fromTag, int toTag, int knotenAnzahl)
        {
            int changedKnoten = 0;

            for(int i = 0; i < knotenList.Count && changedKnoten < knotenAnzahl; i++)
            {
                if (knotenList[i].Tag == fromTag)
                {
                    knotenList[i].Tag = toTag;
                    changedKnoten++;
                }
            }
        }
    }
}
