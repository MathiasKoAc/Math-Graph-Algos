using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class EdmundsKarp
    {

        public double calcMaxFluss(Graph g, Knoten startKnoten, Knoten endKnoten, bool debug = false)
        {
            double flussWert = 0d;

            createResidualGraph(ref g);

            updateResidualGraph(ref g, ref startKnoten, ref endKnoten,debug);

            flussWert = checkUpStartKnoten(startKnoten);

            return flussWert;
        }

        private void createResidualGraph(ref Graph g)
        {
            // am Start ist die RestKapa jeder ResiKante 0 also Fluss = Kapa
            // Jede Kante hat eine ResiKante
            Kante residualKante;
            foreach(Kante kant in g.Kanten)
            {
                residualKante = new Kante(kant.ToKnoten, kant.FromKnoten, kant.Kapazitaet, kant.Kapazitaet, true, kant);
                kant.ToKnoten.AddResidualKante(residualKante);
            }
        }

        private double checkUpStartKnoten(Knoten startKnoten)
        {
            double flussWert = 0d;
            foreach(Kante kant in startKnoten.Kanten)
            {
                flussWert += kant.Fluss;
            }
            return flussWert;
        }

        private void updateResidualGraph(ref Graph g,ref Knoten startKnoten,ref Knoten endKnoten, bool debug = false)
        {
            //WHILE ( ES GIBT EIN WEG :) )
            while(breit(ref g, ref startKnoten, ref endKnoten))
            {
                if(debug) { Console.WriteLine("---"); }

                List<Kante> weg;
                double muee = createWay(ref startKnoten, ref endKnoten, out weg);

                for (int i = weg.Count - 1; i >= 0; i--)
                {
                    weg[i].Fluss += muee;
                    weg[i].ResiKante.Fluss -= muee;
                    if (debug) { Console.WriteLine("Kante: " + weg[i] + " #resi=" + weg[i].IsResidualKante); }
                }
            }            
        }

        private double createWay(ref Knoten startKnoten, ref Knoten endKnoten, out List<Kante> weg)
        {
            weg = new List<Kante>();

            double muee = Double.MaxValue;
            Knoten fokusKnoten = endKnoten;
            while(fokusKnoten.Wert != startKnoten.Wert)
            {
                if(fokusKnoten.VorgaengerKante.RestKapazitaet < muee)
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

            //Mache solange bis Warteschlange größer als 0 ist
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
            foreach(KeyValuePair<int, Knoten> pairKonten in g.Knoten)
            {
                pairKonten.Value.Tag = -1;
                pairKonten.Value.VorgaengerKante = null;
                pairKonten.Value.Vorgaenger = null;
            }
        }
    }
}
