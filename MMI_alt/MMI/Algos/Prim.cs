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
            List<Kante> umgebungsKanten = new List<Kante>();

            int maxTag = 0;
            double mstSize = 0;
            double tmpMstSize = 0;
            Kante focusKante;

            //starten...
            addKantenVonKnoten(startKnoten, ref umgebungsKanten);

            do
            {
                focusKante = pullKante(ref umgebungsKanten);
                if (focusKante != null)
                {

                    tmpMstSize = addKante(focusKante, ref ZielKanten, ref maxTag);
                    if (tmpMstSize > 0f)
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

        private Kante pullKante(ref List<Kante> sortSet)
        {
            if(sortSet.Count > 0)
            {
                Kante focusKante = sortSet.Min();
                sortSet.Remove(focusKante);
                return focusKante;
            }
            return null;
        }

        private void addKantenVonKnoten(Knoten knot, ref List<Kante> sortSet)
        {
            foreach (Kante kant in knot.Kanten)
            {
                if(kant.ToKnoten.Tag == -1 || kant.FromKnoten.Tag == -1)
                {
                    sortSet.Add(kant);
                }                
            }
        }
    }
}
