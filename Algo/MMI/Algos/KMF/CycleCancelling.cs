using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CycleCancelling
    {
        public double calcKMF(Graph g)
        {
            double initalKosten = calcInitalBfluss(ref g);

            bool zyclus = true;
            while (zyclus)
            {
                Graph resiGra = g.createResidualGraph();
                zyclus = findNegativCycle(ref resiGra, out List<int> knotenIndex);
                wegAnpassen(ref g, knotenIndex);
            }
            

            return 0d;
        }

        private void wegAnpassen(ref Graph g, List<int> knotenIndex)
        {
            double wertAnpassung = double.MaxValue;
            double tmpAnpassung = 0;
            int oldIndex = knotenIndex.Last<int>();
            foreach (int index in knotenIndex)
            {
                tmpAnpassung = g.findKante(oldIndex, index).RestKapazitaet;
                if (tmpAnpassung < wertAnpassung)
                {
                    wertAnpassung = tmpAnpassung;
                }
                oldIndex = index;
            }

            oldIndex = knotenIndex.Last<int>();
            foreach (int index in knotenIndex)
            {
                g.findKante(oldIndex, index).Fluss += wertAnpassung;
            }

        }

        //true wenn Cycle gefunden
        private bool findNegativCycle(ref Graph resiGra, out List<int> knotenIndex)
        {
            resiGra.setupSuperQullenSenke(out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, true);
            bool zyklus = ! (new MoorBellmanFord().ShortestWayTree(resiGra, superQuelle, out List<DijKnoten> wayTree, out Kante ex));

            bool[] checkArray = new bool[wayTree.Count];
            knotenIndex = new List<int>();

            if(zyklus)
            {
                Knoten fokusKnoten = ex.FromKnoten;
                for(int i = 0; i < wayTree.Count && !checkArray[fokusKnoten.Wert]; i++)
                {
                    checkArray[fokusKnoten.Wert] = true;
                    knotenIndex.Add(fokusKnoten.Wert);
                    fokusKnoten = wayTree[fokusKnoten.Wert].VorgangerKnoten;
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
