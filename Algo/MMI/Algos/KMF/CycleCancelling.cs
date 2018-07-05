using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CycleCancelling : AbsKMF
    {
        public double calcKMF(Graph g)
        {
            double initalKosten = calcInitalBfluss(ref g);

            Graph resiGra;
            bool zyclus = true;
            while (zyclus)
            {
                resiGra = g.createResidualGraph();
                zyclus = findNegativCycle(ref resiGra, ref g);
            }

            return this.calcFlussKosten(ref g.kanten);
        }

        //true wenn Cycle gefunden
        private bool findNegativCycle(ref Graph resiGra, ref Graph g)
        {
            resiGra.setupSuperQullenSenke(out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, true);
            bool zyklus = ! (new MoorBellmanFord().ShortestWayTree(resiGra, superQuelle, out List<DijKnoten> wayTree, out Kante ex));

            bool[] checkArray = new bool[wayTree.Count];

            if(zyklus)
            {
                Knoten fokusKnoten = ex.FromKnoten;

                //in den Zyclus laufen
                for(int i = 0; i < wayTree.Count; i++)
                {
                    fokusKnoten = wayTree[fokusKnoten.Wert].VorgangerKnoten;
                }

                //Anpassungs werte herausfinden
                double wertAnpassung = double.MaxValue;
                double tmpAnpassung = 0;
                List<Kante> kantenForAnderung = new List<Kante>();
                bool[] isResi = new bool[wayTree.Count];


                for (int i = 0; i < wayTree.Count && !checkArray[fokusKnoten.Wert]; i++)
                {
                    checkArray[fokusKnoten.Wert] = true;
                    Kante kant = g.findKante(wayTree[fokusKnoten.Wert].VorgangerKnoten, fokusKnoten);
                    if(kant != null)
                    {
                        //hier ist die Kante normal gerichtet
                        tmpAnpassung = kant.RestKapazitaet;
                        isResi[i] = false;
                    } else
                    {
                        //hier ist die Kante residual gerichtet
                        kant = g.findKante(fokusKnoten, wayTree[fokusKnoten.Wert].VorgangerKnoten);
                        tmpAnpassung = kant.Fluss;
                        isResi[i] = true;
                    }
                    kantenForAnderung.Add(kant);

                    if (tmpAnpassung < wertAnpassung)
                    {
                        wertAnpassung = tmpAnpassung;
                    }
                    fokusKnoten = wayTree[fokusKnoten.Wert].VorgangerKnoten;
                }

                //Anpassung durchführen

                for(int i = 0; i < kantenForAnderung.Count; i++)
                {
                    if(isResi[i])
                    {
                        kantenForAnderung[i].Fluss -= wertAnpassung;
                    } else
                    {
                        kantenForAnderung[i].Fluss += wertAnpassung;
                    }
                }
                
            }

            return zyklus;
        }

        private double calcInitalBfluss(ref Graph g)
        {
            g.setupSuperQullenSenke(out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, true);
            new EdmondsKarp().calcMFP(g, superQuelle, superSenke);

            g.delSuperSenke(senken, superSenke);
            g.delSuperQuelle(superQuelle);

            return 0d;
            
        }
    }
}
