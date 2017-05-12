using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class DoubleTree : ICountTSP
    {
        public double roundTripp(Graph g, Knoten startKnoten,  out List<Knoten> tour)
        {
            List<Kante> mst;
            PrimFast prim = new PrimFast();
            double gewicht = prim.CountMST(g, out mst, startKnoten);

            List<Kante> doppel = doppleKantenList(mst);
            g.resetKnotenTag();

            List<Kante> fertig;
            int tagLevel = 0;
            Stack<Knoten> stack = new Stack<Knoten>();
            dfs(startKnoten, stack, doppel, out tour, tagLevel);

            foreach(Knoten kno in tour)
            {
                Console.WriteLine("Tour: " + kno.Wert);
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

            Console.WriteLine("----");
            return doppelKanten;
        }

        private void dfs(Knoten startKnoten, Stack<Knoten> stack, List<Kante> kanten, out List<Knoten> path, int tagLevel)
        {
            Knoten aktKnoten;
            path = new List<Knoten>();

            if(startKnoten != null)
            {                
                Kante kant;
                while ((kant = findeKante(kanten, startKnoten, tagLevel)) != null)
                {
                    kant.Tag = 1;
                    startKnoten.Tag++;
                    stack.Push(kant.ToKnoten);
                }
                while (stack.Count > 0)
                {
                    aktKnoten = stack.Pop();
                    dfs(aktKnoten, stack, kanten, out path, tagLevel);
                    path.Add(startKnoten);
                }
            }
        }

        private List<Kante> sortiereKanten(List<Kante> kanten)
        {
            List<Kante> sortKanten = new List<Kante>();
            int tag = 1;
            Knoten startKnoten = kanten[0].FromKnoten;
            Kante nextKante;
            while ((nextKante = findeKante(kanten, startKnoten, tag)) != null)
            {
                startKnoten = nextKante.ToKnoten;
                sortKanten.Add(nextKante);
            }
            return sortKanten;
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

        private void geheUeberAlleUndLink(List<Kante> kanten)
        {
            Knoten lastKnoten = null;
            foreach(Kante kant in kanten)
            {
                if(lastKnoten == null)
                {
                    Console.WriteLine("Start with null");
                    lastKnoten = kant.ToKnoten;
                } 
                else if(lastKnoten.Wert == kant.FromKnoten.Wert)
                {
                    Console.WriteLine("weiter mit: " + kant.ToString());
                    lastKnoten = kant.ToKnoten;
                }
                else
                {
                    Console.WriteLine("jump: " + kant.ToString());
                    lastKnoten = kant.ToKnoten;
                }
            }
        }

        private void tiefenSucheOfKanten(List<Kante> kanten, out List<Kante> kantenPath)
        {
            Kante startKant = kanten[0];
            startKant.FromKnoten.Tag = 1;
            subTief(startKant, kanten, out kantenPath);
            
            foreach(Kante kant in kantenPath)
            {
                Console.WriteLine("tief:" + kant.ToString());
            }
        }

        private void subTief(Kante startKant, List<Kante> kanten, out List<Kante> kantenPath)
        {
            kantenPath = null;
            if (kanten.Count == 0)
            {
                kantenPath = new List<Kante>();
            }
            else
            {

                foreach (Kante kant in kanten)
                {
                    if (startKant.ToKnoten.Wert == kant.FromKnoten.Wert)
                    {
                        Console.WriteLine("tiefff: " + kant.ToString());
                        List<Kante> neuKanten = new List<Kante>(kanten.ToArray());
                        neuKanten.Remove(kant);
                        subTief(startKant, neuKanten, out kantenPath);
                        kantenPath.Add(kant);
                    }
                }
            }            
        }
        
        private List<Knoten> KantenListToKnotenList(List<Kante> kanten)
        {
            List<Knoten> knotens = new List<Knoten>();

            //erster Knoten
            knotens.Add(kanten[0].FromKnoten);

            foreach (Kante kant in kanten)
            {
                Console.WriteLine(kant.ToString());
                knotens.Add(kant.ToKnoten);
            }
            return knotens;
        }

    }
}
