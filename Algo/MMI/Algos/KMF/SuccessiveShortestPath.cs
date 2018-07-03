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
                hatWeg = findWeg(g.createResidualGraph(), quellen, senken, out List<Knoten> weg);

                if (hatWeg)
                {
                    flussErhoehen(ref g, weg);
                    calcPsydoBalacnce(g);
                    findQuellenSenken(g, out quellen, out senken);
                }
            }
            this.psydoBalance = calcPsydoBalacnce(g);
            return 0d;
        }

        private bool findWeg(Graph resiG, List<Knoten> quellen, List<Knoten> senken, out List<Knoten> weg)
        {
            weg = new List<Knoten>();
            double wegDist = double.PositiveInfinity;
            List<Kante> gKanten = resiG.Kanten;
            List<Knoten> gKnoten = resiG.Knoten;

            Knoten superQuelle = new Knoten(gKnoten.Count);
            Knoten superSenke = new Knoten(gKnoten.Count+1);

            Kante tmpKant = null;
            foreach (Knoten q in quellen)
            {
                tmpKant = new Kante(superQuelle, q, double.PositiveInfinity);
                superQuelle.AddKante(tmpKant);
                gKanten.Add(tmpKant);
            }
            gKnoten.Add(superQuelle);

            for(int i = 0; i < senken.Count; i++)
            {
                tmpKant = new Kante(senken[i], superSenke, double.PositiveInfinity);
                gKnoten[senken[i].Wert].AddKante(tmpKant);
                gKanten.Add(tmpKant);
            }
            gKnoten.Add(superSenke);

            wegDist = new MoorBellmanFord().ShortestWay(resiG, superQuelle, superSenke, out weg);

            return wegDist < double.PositiveInfinity;
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

        private void flussErhoehen(ref Graph g, List<Knoten> weg)
        {
            double erhoehung = double.PositiveInfinity;
            double aenderung = double.PositiveInfinity;
            
            //erhöhung ermitteln
            Knoten von = weg[0];
            for(int i = 1; i < weg.Count; i++) {
                aenderung = von.getToKante(weg[i]).RestKapazitaet;
                if (erhoehung > aenderung)
                {
                    erhoehung = aenderung;
                }
                von = weg[i];
            }

            //erhöhung durchführen weg[0] ist superQuelle weg[last] ist superSenke
            von = g.Knoten[weg[1].Wert];
            for (int i = 2; i < (weg.Count-1); i++)
            {
                von.getToKante(g.Knoten[weg[i].Wert]).Fluss += erhoehung;
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
