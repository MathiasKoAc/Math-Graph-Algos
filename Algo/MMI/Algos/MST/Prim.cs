using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Prim : AbsMST
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

            List<Kante> kantenList = new List<Kante>();
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

        private Kante pullKante(ref List<Kante> sortSet, out Knoten neuerKnoten)
        {
            Kante focusKante = null;
            neuerKnoten = null;

            foreach(Kante kant in sortSet)
            {
                if(focusKante == null)
                {
                    focusKante = kant;
                    if (kant.FromKnoten.Tag > -1 && kant.ToKnoten.Tag == -1)
                    {
                        neuerKnoten = kant.ToKnoten;
                    }
                    else if (kant.ToKnoten.Tag > -1 && kant.FromKnoten.Tag == -1)
                    {
                        neuerKnoten = kant.FromKnoten;
                    }
                }
                else if(kant.CompareTo(focusKante) < 0)
                {
                    if (kant.FromKnoten.Tag > -1 && kant.ToKnoten.Tag == -1)
                    {
                        focusKante = kant;
                        neuerKnoten = kant.ToKnoten;
                    }
                    else if(kant.ToKnoten.Tag > -1 && kant.FromKnoten.Tag == -1)
                    {
                        focusKante = kant;
                        neuerKnoten = kant.FromKnoten;
                    } 
                }
            }
            sortSet.Remove(focusKante);
            return focusKante;
        }

        private void addKantenVonKnoten(Knoten knot, ref List<Kante> sortSet)
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
