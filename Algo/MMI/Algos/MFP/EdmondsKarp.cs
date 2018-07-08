using System;
using System.Collections.Generic;
using System.Text;


namespace MMI.Algos
{
    class EdmondsKarp
    {
        public double calcMFP(ref Graph g, Knoten startKnoten, Knoten endKnoten) {//, out List<Knoten> fluss) {

            prepareGraph(ref g);

            double fluss = 0d;
            do
            {
                fluss = getKuerzesterWeg(g, startKnoten, endKnoten, out List<Kante> weg);
                //GraphOut.writeMessage(weg);
                foreach (Kante kant in weg)
                {
                    kant.Fluss += fluss;
                }
            } while (fluss > 0);

            fluss = 0d;
            foreach(Kante kn in startKnoten.Kanten)
            {
                fluss += kn.Fluss;
            }

            return fluss;
        }

        private double getKuerzesterWeg(Graph g, Knoten startKn, Knoten endKn, out List<Kante> weg)
        {
            bool wegGefunden = this.breitWegFinden(g, startKn, endKn, out Knoten[] lookUpTable);

            double wegFluss = -1;
            weg = new List<Kante>();
            if(wegGefunden)
            {
                Knoten lookKnoten = endKn;
                Kante lookKant;
                while(lookUpTable[lookKnoten.Wert].Wert != startKn.Wert)
                {
                    lookKant = lookUpTable[lookKnoten.Wert].getToKante(lookKnoten);
                    lookKnoten = lookUpTable[lookKnoten.Wert];
                    addKanteToWeg(lookKant, ref wegFluss, ref weg);

                }
                lookKant = startKn.getToKante(lookKnoten);
                addKanteToWeg(lookKant, ref wegFluss, ref weg);
            }

            weg.Reverse();
            return wegFluss;
        }

        //bestimme minimum nicht mitten rein schlecht lesbar
        private void addKanteToWeg(Kante kant, ref double vglWegFluss,ref List<Kante> weg)
        {
            double rest = kant.RestKapazitaet;
            if (rest < vglWegFluss || vglWegFluss < 0)
            {
                vglWegFluss = rest;
            }
            weg.Add(kant);

        }

        private bool breitWegFinden(Graph g, Knoten startKn, Knoten endKn, out Knoten[] lookUpTable)
        {
            g.resetKnotenTag();
            Queue<Knoten> queue = new Queue<Knoten>();
            lookUpTable = new Knoten[g.getAnzKnoten()];

            queue.Enqueue(startKn);
            lookUpTable[startKn.Wert] = startKn;
            Knoten knoten = startKn;
            bool wegGefunden = false;

            //Mache solange Warteschlange größer als 0 ist
            while (queue.Count > 0)
            {
                knoten = queue.Dequeue();

                if (knoten.Wert == endKn.Wert)
                {
                    wegGefunden = true;
                }

                foreach (var kante in knoten.Kanten)
                {
                    if (kante.ToKnoten.Tag == -1 && kante.RestKapazitaet > 0)
                    {
                        queue.Enqueue(kante.ToKnoten);
                        kante.ToKnoten.Tag = 1;
                        lookUpTable[kante.ToKnoten.Wert] = knoten;
                    }
                }
            }
            return wegGefunden;
        }

        private void prepareGraph(ref Graph g)
        {
            g.createResidualKanten();
            g.resetFluss();
        }
    }
}
