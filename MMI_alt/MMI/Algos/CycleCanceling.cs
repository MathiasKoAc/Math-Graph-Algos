using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CycleCanceling
    {
        private EdmundsKarp edmundKarp = new EdmundsKarp();
        private BellmanFord bellmanFord = new BellmanFord();

        void calcKostenMinimalerFluss(Graph gra)
        {
            SuperQuelleSenke(ref gra, out Knoten Quelle, out Knoten Senke);

            edmundKarp.calcMaxFluss(gra, Quelle, Senke);
        }

        private void SuperQuelleSenke(ref Graph gra, out Knoten Quelle, out Knoten Senke)
        {
            Quelle = new Knoten(1000000);
            Senke = new Knoten(1000001);

            Kante neueKante = null;

            for(int i = 0; i < gra.Knoten.Count; i++) 
            {
                if (gra.Knoten[i].Balance > 0)
                {
                    neueKante = new Kante(Quelle, gra.Knoten[i]);
                    neueKante.Kapazitaet = gra.Knoten[i].Balance;
                    neueKante.Kosten = 0;
                    Quelle.Kanten.Add(neueKante);
                    gra.Kanten.Add(neueKante);
                }
                else if (gra.Knoten[i].Balance < 0)
                {
                    neueKante = new Kante(gra.Knoten[i], Senke);
                    neueKante.Kapazitaet = gra.Knoten[i].Balance;
                    neueKante.Kosten = 0;
                    gra.Knoten[i].AddKante(neueKante);
                    gra.Kanten.Add(neueKante);
                }
            }
        }

        private void createResidualGraph(ref Graph g)
        {
            // am Start ist die RestKapa jeder ResiKante 0 also Fluss = Kapa
            // Jede Kante hat eine ResiKante
            Kante residualKante;
            foreach (Kante kant in g.Kanten)
            {
                residualKante = new Kante(kant.ToKnoten, kant.FromKnoten, kant.Kapazitaet, kant.Kapazitaet, true, kant);
                residualKante.Kosten = kant.Kosten * -1;
                kant.ToKnoten.AddResidualKante(residualKante);
            }
        }

        private void updateResidualGraph(ref Graph g, ref Knoten startKnoten, ref Knoten endKnoten, bool debug = false)
        {
            //WHILE ( ES GIBT EIN WEG :) )
            while (breit(ref g, ref startKnoten, ref endKnoten))
            {
                if (debug) { Console.WriteLine("---"); }

                List<Kante> weg;
                double muee = createWay(ref startKnoten, ref endKnoten, out weg);

                if(bellmanFord.sortestWay(ref g, startKnoten, out Knoten ZyklusCheckKnoten)) {
                    //kein Negativer Zykel
                    Console.WriteLine("//kein Negativer Zykel");
                    return;
                } else
                {
                    if(zyklusFinden(ZyklusCheckKnoten, g.Knoten.Count, out List<Knoten> zyklus, out double minKap))
                    {
                        //mit dem Zyklus den Fluss anpassen

                    }
                    else
                    {
                        //error
                    }
                }

                for (int i = weg.Count - 1; i >= 0; i--)
                {
                    //hier muss keine unterscheidung zwischen Kanten und ResiKante gemacht werden weil auch die Resikante ein Fluss und ein Kapa hat
                    weg[i].Fluss += muee;
                    weg[i].ResiKante.Fluss -= muee;
                    if (debug) { Console.WriteLine("Kante: " + weg[i] + " #resi=" + weg[i].IsResidualKante); }
                }
            }
        }

        private bool zyklusFinden(Knoten bellmanCheckKnoten, int count, out List<Knoten> zykus, out double minKap)
        {
            List<int> wdh = new List<int>();
            zykus = new List<Knoten>();
            Knoten fokusKnoten = bellmanCheckKnoten;
            int doppelter = -1;

            minKap = Double.MaxValue;

            for(int i = 0; i < count && doppelter == -1; i++)
            {
                if(wdh.Contains(fokusKnoten.Wert))
                {
                    //zyklus gefunden
                    doppelter = fokusKnoten.Wert;
                } else
                {
                    wdh.Add(fokusKnoten.Wert);
                }
                fokusKnoten = fokusKnoten.Vorgaenger;
            }


            bool startZyklus = false;
            fokusKnoten = bellmanCheckKnoten;
            for (int i = 0; i < count; i++)
            {
                if(fokusKnoten.Wert == doppelter)
                {
                    startZyklus = !startZyklus;
                }
                if (startZyklus)
                {
                    zykus.Add(fokusKnoten);
                    if(fokusKnoten.VorgaengerKante.Kapazitaet < minKap)
                    {
                        minKap = fokusKnoten.VorgaengerKante.Kapazitaet;
                    }
                }
                fokusKnoten = fokusKnoten.Vorgaenger;
            }

            return (zykus.Count > 0);
        }

        private double createWay(ref Knoten startKnoten, ref Knoten endKnoten, out List<Kante> weg)
        {
            weg = new List<Kante>();

            double muee = Double.MaxValue;
            Knoten fokusKnoten = endKnoten;

            while (fokusKnoten.Wert != startKnoten.Wert)
            {
                if (fokusKnoten.VorgaengerKante.RestKapazitaet < muee)
                {
                    muee = fokusKnoten.VorgaengerKante.RestKapazitaet;
                }

                weg.Add(fokusKnoten.VorgaengerKante);
                fokusKnoten = fokusKnoten.Vorgaenger;
            }

            return muee;
        }

        private bool breit(ref Graph g, ref Knoten startKnoten, ref Knoten endKnoten)
        {
            int visited = 1;
            Queue<Knoten> queue = new Queue<Knoten>();

            resetGraph(ref g);

            queue.Enqueue(startKnoten);
            startKnoten.Tag = visited;
            startKnoten.Vorgaenger = startKnoten;

            Knoten fokusKnoten = startKnoten;

            //Mache solange Warteschlange größer als 0 ist
            while (queue.Count > 0 && fokusKnoten.Wert != endKnoten.Wert)
            {
                fokusKnoten = queue.Dequeue();

                foreach (var kante in fokusKnoten.Kanten)
                {
                    if (kante.RestKapazitaet > 0 && kante.ToKnoten.Tag == -1)
                    {
                        queue.Enqueue(kante.ToKnoten);
                        kante.ToKnoten.Tag = visited;
                        kante.ToKnoten.Vorgaenger = fokusKnoten;
                        kante.ToKnoten.VorgaengerKante = kante;
                    }
                }

                foreach (var kante in fokusKnoten.ResidualKanten)
                {
                    if (kante.RestKapazitaet > 0 && kante.ToKnoten.Tag == -1)
                    {
                        queue.Enqueue(kante.ToKnoten);
                        kante.ToKnoten.Tag = visited;
                        kante.ToKnoten.Vorgaenger = fokusKnoten;
                        kante.ToKnoten.VorgaengerKante = kante;
                    }
                }
            }

            //runToEndKnoten...
            return fokusKnoten == endKnoten;
        }

        private void resetGraph(ref Graph g)
        {
            foreach (KeyValuePair<int, Knoten> pairKonten in g.Knoten)
            {
                pairKonten.Value.Tag = -1;
                pairKonten.Value.VorgaengerKante = null;
                pairKonten.Value.Vorgaenger = null;
            }
        }
    }
}
