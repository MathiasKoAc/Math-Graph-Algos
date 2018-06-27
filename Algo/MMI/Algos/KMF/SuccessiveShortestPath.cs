using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class SuccessiveShortestPath
    {
        protected double[] psydoBalance;

        public double calcKMF(Graph g)
        {
            resetPsydoBalance(g.getAnzKnoten());
            setzteStartFluss(ref g.Kanten);
            this.psydoBalance = calcPsydoBalacnce(g);
            findQuellenSenken(g, out List<Knoten> quellen, out List<Knoten> senken);
            bool hatWeg = true;
            while (hatWeg && quellen.Count > 0 && senken.Count > 0)
            {
                hatWeg = findWeg(g, quellen, senken, out List<Knoten> weg);

                if (hatWeg)
                {
                    flussErhoehen(weg.First<Knoten>(), weg.Last<Knoten>(), weg);
                    calcPsydoBalacnce(g);
                    findQuellenSenken(g, out quellen, out senken);
                }
            }
            this.psydoBalance = calcPsydoBalacnce(g);
            return 0d;
        }

        private bool findWeg(Graph g, List<Knoten> quellen, List<Knoten> senken, out List<Knoten> weg)
        {
            bool hatWeg = false;
            weg = new List<Knoten>();
            double wegDist = double.PositiveInfinity;

            for(int i = 0; !hatWeg && i < quellen.Count; i++)
            {
                for(int y = 0; !hatWeg && y < senken.Count; y++)
                {
                    Knoten von = quellen[i];
                    Knoten bis = senken[y];
                    wegDist = new MoorBellmanFord().ShortestWay(g, von, bis, out weg);

                    hatWeg = (wegDist < double.PositiveInfinity && getMinRestKapazitaet(weg) > 0);
                }
            }
            return hatWeg;
        }

        private double getMinRestKapazitaet(List<Knoten> weg)
        {
            double minRestKapa = double.PositiveInfinity;
            double tmp = 0;

            Knoten von = weg[0];
            for (int i = 1; i < weg.Count; i++)
            {
                tmp = von.getToKante(weg[i]).RestKapazitaet;
                if (minRestKapa > tmp)
                {
                    minRestKapa = tmp;
                }
                von = weg[i];
            }
            return minRestKapa;
        }

        private void flussErhoehen(Knoten Quelle, Knoten Senke, List<Knoten> weg)
        {
            double erhoehung = Quelle.Balance - psydoBalance[Quelle.Wert];
            double aenderung = psydoBalance[Senke.Wert] - Senke.Balance;
            if (erhoehung > aenderung)
            {
                erhoehung = aenderung;
            }
            Knoten von = weg[0];
            for(int i = 1; i < weg.Count; i++) {
                aenderung = von.getToKante(weg[i]).RestKapazitaet;
                if (erhoehung > aenderung)
                {
                    erhoehung = aenderung;
                }
                von = weg[i];
            }

            von = weg[0];
            for (int i = 1; i < weg.Count; i++)
            {
                von.getToKante(weg[i]).Fluss += erhoehung;
                
            }

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

            for (int i = 0; i < psydoBalance.Length; i++)
            {
                if (psydoBalance[i] > g.Knoten[i].Balance)
                {
                    Senken.Add(g.Knoten[i]);
                } else if (psydoBalance[i] < g.Knoten[i].Balance)
                {
                    Quellen.Add(g.Knoten[i]);
                }
            } 
        }

        private double[] calcPsydoBalacnce(Graph g)
        {
            // Knoten muss einem Ausfluss - Einfluss > 0 sind positiv
            double[] d = new double[g.Knoten.Count];

            List<Knoten> knotenList = g.Knoten;
            for(int i = 0; i < g.Knoten.Count; i++)
            {
                d[g.Knoten[i].Wert] = g.Knoten[i].calcAusfluss();
            }

            for (int i = 0; i < g.Kanten.Count; i++)
            {
                d[g.Kanten[i].ToKnoten.Wert] -= g.Kanten[i].Fluss;
            }
            return d;
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
