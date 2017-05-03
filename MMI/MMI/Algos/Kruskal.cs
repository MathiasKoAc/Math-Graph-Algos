using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Kruskal : ICountMST
    {
        public int CountMST(Graph Gra)
        {
            int maxTag = 0;
            List<Kante> Kanten = new List<Kante>();
        }

        private bool addKante(ref Kante k, ref List<Kante> Kanten, ref int maxTag)
        {
            bool added = false;
            Knoten toKnoten = k.ToKnoten;
            Knoten fromKnoten = k.FromKnoten;


            //neue Kante
            if (toKnoten.Tag == -1 || fromKnoten.Tag == -1)
            {
                toKnoten.Tag = maxTag;
                fromKnoten.Tag = maxTag;
                maxTag++;
                Kanten.Add(k);
                added = true;
            }
            else if (toKnoten.Tag > fromKnoten.Tag)
            {
                // andere alle Knoten mit dem toTag auf das Tag vom fromKnoten
                changeKanteTag(ref Kanten, toKnoten.Tag, fromKnoten.Tag);
                Kanten.Add(k);
                added = true;
            }
            else if (toKnoten.Tag < fromKnoten.Tag)
            {
                // andere alle Knoten mit dem fromTag auf das Tag vom toKnoten
                changeKanteTag(ref Kanten, fromKnoten.Tag, toKnoten.Tag);
                Kanten.Add(k);
                added = true;
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
                if(kant.Tag == fromTag)
                {
                    kant.Tag = toTag;
                }
            }
        }
    }
}
