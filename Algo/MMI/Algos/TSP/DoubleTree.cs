using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class DoubleTree : ICountTSP
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

            List<Kante> fertig;
            int tagLevel = 0;
            Stack<Kante> stack = new Stack<Kante>();
            treeRun = new List<Kante>();
            Console.WriteLine("\n----");
            KantenTiefenSuche(findeKante(g.Kanten, startKnoten, 0), stack, doppelList, out fertig, tagLevel);

            gewicht = tourAusgebenRechen(this.treeRun);
            Console.WriteLine("Tour-gewicht: " + gewicht);
            Console.WriteLine("---");
            List<Kante> kantenOpt = dreieckOptimierung(g, treeRun);
            gewicht = tourAusgebenRechen(kantenOpt);
            Console.WriteLine("Tour-gewicht nach Dreieck: " + gewicht);

            tour = KantenListToKnotenList(treeRun);

            return gewicht;
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

        private void KantenTiefenSuche(Kante startKante, Stack<Kante> stack, List<Kante> kanten, out List<Kante> path, int tagLevel)
        {
            Kante aktKante;
            path = new List<Kante>();

            if(startKante != null)
            {
                treeRun.Add(startKante);
                Kante kant;
                while ((kant = findeKante(kanten, startKante.ToKnoten, tagLevel)) != null)
                {
                    kant.Tag = 1;
                    startKante.Tag++;
                    stack.Push(kant);
                }
                while (stack.Count > 0)
                {
                    aktKante = stack.Pop();
                    KantenTiefenSuche(aktKante, stack, kanten, out path, tagLevel);
                    path.Add(startKante);
                }
            }
        }

        private Kante findeKante(List<Kante> kanten, Knoten fromKnoten, int tag)
        {
            Kante findKant = null;
            foreach(Kante kant in kanten)
            {
                if(kant.FromKnoten.Wert == fromKnoten.Wert && kant.Tag == -1 && kant.ToKnoten.Tag < tag)
                {
                    findKant = kant;
                    kant.Tag = tag;
                    break;
                }
            }
            return findKant;
        }
                
        private List<Knoten> KantenListToKnotenList(List<Kante> kanten)
        {
            List<Knoten> knotens = new List<Knoten>();

            //erster Knoten
            knotens.Add(kanten[0].FromKnoten);

            foreach (Kante kant in kanten)
            {
                knotens.Add(kant.ToKnoten);
            }
            return knotens;
        }

    }
}
