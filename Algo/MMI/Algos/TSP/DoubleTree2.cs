using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Algos.Traversieren;

namespace MMI.Algos
{
    class DoubleTree2 : ICountTSP
    {

        public double roundTripp(Graph g, Knoten startKnoten,  out List<Knoten> tour)
        {
            List<Kante> mst;
            Kruskal krus = new Kruskal();
            double gewicht = krus.CountMST(g, out mst);
            List<Kante> doppelList = doppleKantenList(mst);
            Console.WriteLine("MST-Gewicht: " + gewicht);
            g.resetKnotenTag();
            g.resetKantenTag();

            Graph doppelTreeGraph = Graph.createInstance(doppelList);
            List<Knoten> knotenReihenFolge = new Tiefensuche().durchlaufen(doppelTreeGraph.Knoten[0]);
            knotenReihenFolge.Add(knotenReihenFolge[0]);
            GraphOut.writeMessage("Knoten Reihenfolge:");
            GraphOut.writeMessage(knotenReihenFolge);
            gewicht = createTourMitAbkuertzung(g, knotenReihenFolge, out List<Kante> kantenList);

            GraphOut.writeMessage("Kanten Reihenfolge:");
            GraphOut.writeMessage(kantenList);
            Console.WriteLine("Naiv-DoppelTree-Gewicht: " + gewicht);

            /*
            List<Kante> dreiecksList = dreieckOptimierung(g, kantenList);
            Console.WriteLine("Dreieck Reihenfolge");
            gewicht = tourAusgebenRechen(dreiecksList);
            */

            tour = knotenReihenFolge;
            return gewicht;
        }

        private double createTourMitAbkuertzung(Graph g, List<Knoten> knotenReihenfolge, out List<Kante> kantenList)
        {
            double wert = 0;
            kantenList = new List<Kante>();

            Knoten startKnoten = knotenReihenfolge[0];

            for (int i = 1; i < knotenReihenfolge.Count; i++)
            {
                kantenList.Add(g.findKante(startKnoten.Wert, knotenReihenfolge[i].Wert));
                startKnoten = knotenReihenfolge[i];
                wert += kantenList.Last<Kante>().Gewicht;
            }

            return wert;
        }

        private double tourAusgebenRechen(List<Kante> kantens)
        {
            double gewicht = 0;
            foreach (Kante kan in kantens)
            {
                if(kan != null)
                {
                    Console.WriteLine("Tour: " + kan.ToString());
                    gewicht += kan.Gewicht;
                }                
            }
            return gewicht;
        }

        private List<Kante> doppleKantenList(List<Kante> kanten)
        {
            List<Kante> doppelKanten = new List<Kante>();

            foreach (Kante kant in kanten)
            {
                Console.WriteLine(kant.ToString());
                kant.Tag = -1;
                doppelKanten.Add(kant);
                doppelKanten.Add(new Kante(kant.ToKnoten, kant.FromKnoten, kant.Gewicht));
            }
            return doppelKanten;
        }

    }
}
