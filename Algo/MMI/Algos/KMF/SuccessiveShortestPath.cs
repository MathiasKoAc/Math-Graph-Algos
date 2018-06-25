using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos.KMF
{
    class SuccessiveShortestPath
    {
        double[] psydoBalance;

        public double calcKMF(Graph g, out List<Kante> KantenListFluss)
        {
            resetPsydoBalance(g.getAnzKnoten());
            setzteStartFluss(ref g.Kanten);
            calcPsydoBalacnce(g);
            findQuellenSenken(g, out List<Knoten> quellen, out List<Knoten> senken);

            
        }

        private void setzteStartFluss(ref List<Kante> kantenList)
        {
            foreach(Kante k in kantenList)
            {
                if(k.Kosten < 0)
                {
                    k.setMaxFluss();
                }
            }
        }

        private void findQuellenSenken(Graph g, out List<Knoten> Quellen, out List<Knoten> Senken)
        {
            Quellen = new List<Knoten>();
            Senken = new List<Knoten>();

            for(int i = 0; i < psydoBalance.Length; i++)
            {
                if (psydoBalance[i] < 0)
                {
                    Senken.Add(g.Knoten[i]);
                } else if (psydoBalance[i] > 0)
                {
                    Quellen.Add(g.Knoten[i]);
                }
            } 
        }

        private void calcPsydoBalacnce(Graph g)
        {
            // Knoten muss einem Ausfluss - Einfluss > 0 sind positiv

            List<Knoten> knotenList = g.Knoten;
            foreach(Knoten knot in knotenList)
            {
                psydoBalance[knot.Wert] = knot.calcAusfluss();    
            }

            foreach(Kante kant in g.Kanten)
            {
                psydoBalance[kant.ToKnoten.Wert] = -kant.Fluss;
            }
        }

        private void resetPsydoBalance(int count)
        {
            psydoBalance = new double[count];
            
            for(int i = 0; i < count; i++)
            {
                psydoBalance[i] = 0;
            }
        }

    }
}
