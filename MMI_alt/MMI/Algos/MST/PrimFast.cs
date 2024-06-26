﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class PrimFast : AbsMST
    {

        public override double CountMST(Graph Gra, out List<Kante> Kanten)
        {
            return CountMST(Gra, out Kanten, Gra.Knoten[0]);
        }

        public double CountMST(Graph Gra, out List<Kante> ZielKanten, Knoten startKnoten)
        {
            int knotenMaxAnz = Gra.Knoten.Count;
            int knotenCount = 0;

            ZielKanten = new List<Kante>();
            InsertSortList umgebungsKanten = new InsertSortList();

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

        private Kante pullKante(ref InsertSortList sortSet)
        {
            if(!sortSet.IsLeer())
            {
                return sortSet.PullMin();
            }
            return null;
        }

        private void addKantenVonKnoten(Knoten knot, ref InsertSortList sortSet)
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
