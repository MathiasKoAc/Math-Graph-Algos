using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Prim : AbsMST
    {

        public override double CountMST(Graph Gra)
        {
            return CountMST(Gra, Gra.Konten[0]);
        }

        public double CountMST(Graph Gra, Knoten startKnoten)
        {
            int knotenMaxAnz = Gra.Konten.Count;
            int knotenCount = 0;

            List<Kante> ZielKanten = new List<Kante>();
            SortedSet<Kante> umgebungsKanten = new SortedSet<Kante>();

            int maxTag = 0;
            double mstSize = 0;
            double tmpMstSize = 0;
            Kante focusKante;

            //starten...
            addKantenVonKnoten(startKnoten, ref umgebungsKanten);

            int debug = 0;

            do
            {
                focusKante = pullKante(ref umgebungsKanten);
                if (focusKante != null)
                {
                    System.Console.WriteLine("#" + debug++);
                    tmpMstSize = addKante(focusKante, ref ZielKanten, ref maxTag);
                    if (tmpMstSize > 0)
                    {
                        knotenCount++;
                        mstSize += tmpMstSize;
                        addKantenVonKnoten(focusKante.ToKnoten, ref umgebungsKanten);
                        addKantenVonKnoten(focusKante.FromKnoten, ref umgebungsKanten);
                    }
                }

            } while (knotenCount < knotenMaxAnz && focusKante != null);

            return mstSize;
        }

        private Kante pullKante(ref SortedSet<Kante> sortSet)
        {
            Kante focusKante = sortSet.Min;
            sortSet.Remove(focusKante);
            return focusKante;
        }

        private void addKantenVonKnoten(Knoten knot, ref SortedSet<Kante> sortSet)
        {
            foreach (Kante kant in knot.Kanten)
            {
                sortSet.Add(kant);               
            }
        }
    }
}
