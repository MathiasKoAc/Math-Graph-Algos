using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Kruskal : ICountMST
    {
        public double CountMST(Graph Gra)
        {
            int maxTag = 0;
            List<Kante> Kanten = new List<Kante>();
            double mstSize = 0;

            foreach(Kante k in Gra.Kanten)
            {
                mstSize += addKante(k, ref Kanten, ref maxTag);
            }
            return mstSize;
        }

        private double addKante(Kante k, ref List<Kante> Kanten, ref int maxTag)
        {
            double added = 0.0;
            Knoten toKnoten = k.ToKnoten;
            Knoten fromKnoten = k.FromKnoten;
            
            /*
            System.Console.WriteLine("");
            System.Console.WriteLine("Kante " + fromKnoten.Wert + " -> " + toKnoten.Wert);
            System.Console.WriteLine("Tag from " + fromKnoten.Tag + " # Tag to " + toKnoten.Tag); 
            */

            //neue lose Kante
            if (toKnoten.Tag == -1 && fromKnoten.Tag == -1)
            {
                maxTag++;
                toKnoten.Tag = maxTag;
                fromKnoten.Tag = maxTag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if(toKnoten.Tag == -1 && fromKnoten.Tag >= 0)   //neue Kante toKnoten neu
            {
                toKnoten.Tag = fromKnoten.Tag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if(fromKnoten.Tag == -1 && toKnoten.Tag >= 0)   //neue Kante FromKonten neu
            {
                fromKnoten.Tag = toKnoten.Tag;
                Kanten.Add(k);
                added = k.Gewicht;
            }
            else if (toKnoten.Tag > fromKnoten.Tag)     //fromKnoten alter Graph
            {
                // andere alle Knoten mit dem toTag auf das Tag vom fromKnoten
                Kanten.Add(k);
                changeKanteTag(ref Kanten, toKnoten.Tag, fromKnoten.Tag);
                added = k.Gewicht;
            }
            else if (toKnoten.Tag < fromKnoten.Tag)     //toKnoten alter Graph
            {
                // andere alle Knoten mit dem fromTag auf das Tag vom toKnoten
                Kanten.Add(k);
                changeKanteTag(ref Kanten, fromKnoten.Tag, toKnoten.Tag);
                added = k.Gewicht;
            }
            return added;
        }

        /// <summary>
        /// Aendert das Tag der Kanten in der List mit dem Tag fromTag auf den Wert toTag
        /// </summary>
        /// <param name="Kanten">ref List von Kanten</param>
        /// <param name="fromTag">zu suchender Tag</param>
        /// <param name="toTag">Tag auf den geandert werden soll</param>
        private void changeKanteTag(ref List<Kante> Kanten, int fromTag, int toTag)
        {
            foreach(Kante kant in Kanten)
            {
                if(kant.FromKnoten.Tag == fromTag)
                {
                    kant.FromKnoten.Tag = toTag;
                }

                if (kant.ToKnoten.Tag == fromTag)
                {
                    kant.ToKnoten.Tag = toTag;
                }
            }
        }
    }
}
