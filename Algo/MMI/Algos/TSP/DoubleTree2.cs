using System;
using System.Collections.Generic;
using System.Linq;
using MMI.Algos.Traversieren;

namespace MMI.Algos
{
    class DoubleTree2 : ICountTSP
    {
        List<Kante> treeRun;

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
            //GraphOut.writeMessage("Knoten:");
            //GraphOut.writeMessage(doppelTreeGraph.Knoten);
            //GraphOut.writeMessage("Kanten:");
            //GraphOut.writeMessage(doppelTreeGraph.Kanten);
            List<Knoten> knotenReihenFolge = new Tiefensuche().durchlaufen(doppelTreeGraph.Knoten[0]);
            GraphOut.writeMessage("Knoten Reihenfolge:");
            GraphOut.writeMessage(knotenReihenFolge);
            gewicht = createTourMitAbkuertzung(g, knotenReihenFolge, out List<Kante> kantenList);
            GraphOut.writeMessage("Kanten Reihenfolge:");
            GraphOut.writeMessage(kantenList);
            Console.WriteLine("Naiv-DoppelTree-Gewicht: " + gewicht);
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

            //Kreis schließen
            Kante letzteKante = g.findKante(startKnoten.Wert, knotenReihenfolge[0].Wert);
            kantenList.Add(letzteKante);
            wert += letzteKante.Gewicht;

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

        private List<Kante> dreieckOptimierung(Graph g, List<Kante> kanten)
        {
            List<Knoten> Knotens = new List<Knoten>();
            List<Kante> Kantens = new List<Kante>();

            Knotens.Add(kanten[0].FromKnoten);
            bool added = false;
            int startI;
            for (int i = 0; i < kanten.Count; i++)
            {
                added = false;
                startI = i;

                var toK = kanten[i].ToKnoten;
                if (Knotens.Contains(kanten[i].ToKnoten))
                {
                    //umweg
                    double umweg = 0;
                    //gibt nachste Kante wo toKnoten ein neuer ist
                    for(; i < kanten.Count && !added; )
                    {
                        umweg += kanten[i].Gewicht;
                        if(!Knotens.Contains(kanten[i].ToKnoten))
                        {
                            Kante schleichweg = g.findKante(kanten[startI].FromKnoten, kanten[i].ToKnoten);
                            if(schleichweg != null && schleichweg.Gewicht < umweg)
                            {
                                Knotens.Add(kanten[startI].ToKnoten);
                                Kantens.Add(schleichweg);
                                added = true;
                            }
                        }
                        
                        if(!added)
                        {
                            i++;
                        }
                    }
                }

                //Normal weg
                if(!added && i < kanten.Count)
                {
                    Knotens.Add(kanten[startI].ToKnoten);
                    Kantens.Add(kanten[startI]);
                }
            }

            //Rückweg
            Kantens.Add(g.findKante(Knotens.Last(), Knotens.First()));
            return Kantens;
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
