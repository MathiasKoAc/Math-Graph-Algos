using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Prim2 : ICountMST
    {

        public double CountMST(Graph Gra, out List<Kante> Kanten)
        {
            return CountMST(Gra, out Kanten, Gra.Knoten[0]);
        }

        public double CountMST(Graph Gra, out List<Kante> ZielKanten, Knoten startKnoten)
        {
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
                if (fokusKante == null)
                {
                    throw new Exception("Graph nicht zusammen hängend.");
                }
                ZielKanten.Add(fokusKante);
                mstWert += fokusKante.Gewicht;
                addKantenVonKnoten(neuerKnoten, ref kantenList);
                knotenCounter++;
            }
            return mstWert;
        }

        private Kante pullKante(ref SortedSet<Kante> sortSet, out Knoten neuerKnoten)
        {
            neuerKnoten = null;
            Kante focusKante = null;

            while(focusKante == null && sortSet.Count() > 0)
            {
                Kante tmpFocus = sortSet.Min;
                sortSet.Remove(tmpFocus);

                if (tmpFocus.FromKnoten.Tag > -1 && tmpFocus.ToKnoten.Tag == -1)
                {
                    neuerKnoten = tmpFocus.ToKnoten;
                    focusKante = tmpFocus;
                }
                else if (tmpFocus.ToKnoten.Tag > -1 && tmpFocus.FromKnoten.Tag == -1)
                {
                    neuerKnoten = focusKante.FromKnoten;
                    focusKante = tmpFocus;
                }
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
                    bool addedSuccessful = false;
                    while(!addedSuccessful)
                    {
                        addedSuccessful = sortSet.Add(kant);
                        if(!addedSuccessful)
                        {
                            //TODO Scalierbar machen
                            kant.Offset += (0.0000001);
                        }
                    }                    
                }
            }
        }
    }
}
