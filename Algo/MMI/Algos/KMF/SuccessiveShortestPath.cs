using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class SuccessiveShortestPath : AbsKMF
    {
        protected double[] psydoBalance;

        public double calcKMF(Graph g)
        {
            resetPsydoBalance(g.getAnzKnoten());
            setzteStartFluss(ref g.Kanten);
            this.psydoBalance = calcPsydoBalacnce(g);
            findQuellenSenken(g, out List<Knoten> quellen, out List<Knoten> senken);
            bool hatWeg = true;
            Graph resi = null;
            while (hatWeg && quellen.Count > 0 && senken.Count > 0)
            {
                resi = g.createResidualGraph();
                hatWeg = findWeg(resi, quellen, senken, out List<Knoten> weg);

                if (hatWeg)
                {
                    flussErhoehen(ref g, weg);
                    this.psydoBalance = calcPsydoBalacnce(g);
                    findQuellenSenken(g, out quellen, out senken);
                }
            }
            this.psydoBalance = calcPsydoBalacnce(g);

            if(!hatWeg)
            {
                throw new NotBflussException("Es konnte kein B-Fluss gefunden werden.");
            }
            return calcFlussKosten(ref g.kanten);
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

        private void flussErhoehen(ref Graph g, List<Knoten> weg)
        {
            Knoten quelle = g.Knoten[weg[1].Wert];
            Knoten senke = g.Knoten[weg[weg.Count - 2].Wert];

            double erhoehung = quelle.Balance - psydoBalance[quelle.Wert];
            double aenderung = psydoBalance[senke.Wert] - senke.Balance;

            if (erhoehung > aenderung)
            {
                erhoehung = aenderung;
            }

            //TODO HIER ÄNDERN!!!!

            //erhöhung ermitteln
            Knoten von = weg[1];
            for (int i = 2; i < weg.Count-1; i++) {
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
                von.getToKante(weg[i]).Fluss += erhoehung;
                von = g.Knoten[weg[i].Wert];
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
