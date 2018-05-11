using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Prim2 : AbsMST
    {

        public override double CountMST(Graph Gra, out List<Kante> Kanten)
        {
            return CountMST(Gra, out Kanten, Gra.Knoten[0]);
        }

        public double CountMST(Graph Gra, out List<Kante> ZielKanten, Knoten startKnoten)
        {
            Gra.resetKantenTag();
            Gra.resetKnotenTag();

            int goalKnotenCount = Gra.Knoten.Count;
            int knotenCounter = 1;

            SortedSet<Kante> kantenList = new SortedSet<Kante>();
            ZielKanten = new List<Kante>();
            startKnoten.Tag = 1;
            addKantenVonKnoten(startKnoten, ref kantenList);
            

            double mstWert = 0;

            while(knotenCounter < goalKnotenCount)
            {
                var fokusKante = pullKante(ref kantenList, out Knoten neuerKnoten);
                ZielKanten.Add(fokusKante);
                mstWert += fokusKante.Gewicht;
                if(neuerKnoten == null)
                {
                    break;
                }
                addKantenVonKnoten(neuerKnoten, ref kantenList);
                knotenCounter++;
            }
            return mstWert;
        }

        private Kante pullKante(ref SortedSet<Kante> sortSet, out Knoten neuerKnoten)
        {
            neuerKnoten = null;
            Kante focusKante = sortSet.Min;
            sortSet.Remove(focusKante);

            if (focusKante.FromKnoten.Tag > -1 && focusKante.ToKnoten.Tag == -1)
            {
                neuerKnoten = focusKante.ToKnoten;
            }
            else if (focusKante.ToKnoten.Tag > -1 && focusKante.FromKnoten.Tag == -1)
            {
                neuerKnoten = focusKante.FromKnoten;
            }

            return focusKante;
        }

        private void addKantenVonKnoten(Knoten knot, ref SortedSet<Kante> sortSet)
        {
            knot.Tag = 1;
            foreach (Kante kant in knot.Kanten)
            {
                if (kant.ToKnoten.Tag == -1 || kant.FromKnoten.Tag == -1)
                {
                    sortSet.Add(kant);
                }
            }
        }
    }
}
