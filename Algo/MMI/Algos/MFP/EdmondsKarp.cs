using System;
using System.Collections.Generic;
using System.Text;


namespace MMI.Algos
{
    class EdmondsKarp
    {
        public double calcMFP(Graph g, Knoten startKnoten, Knoten endKnoten, out List<Knoten> fluss) {

            
            return 0d;
        }

        private List<Kante> getKuerzesterWeg(Knoten startKn, Knoten endKn)
        {
            Queue<Knoten> queue = new Queue<Knoten>();
            queue.Enqueue(startKn);

            //Mache solange Warteschlange größer als 0 ist
            while (queue.Count > 0)
            {
                Knoten knoten = queue.Dequeue();

                foreach (var kante in knoten.Kanten)
                {
                    if (kante.ToKnoten.Tag == -1)
                    {
                        queue.Enqueue(kante.ToKnoten);
                        kante.ToKnoten.Tag = tagLv;
                    }
                    else if (kante.ToKnoten.Tag < tagLv)
                    {
                        neuerZHK = false;
                    }
                }
            }

            return neuerZHK;
        }

        private List<Kante> prepareGraph(Graph g)
        {
            g.resetKnotenTag();
            g.resetFluss();
            return g.ResidualKanten;
        }
    }
}
