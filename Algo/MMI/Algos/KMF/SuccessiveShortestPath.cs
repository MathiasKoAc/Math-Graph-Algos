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
            setzteStartFluss(ref g.Kanten);
            this.psydoBalance = calcPsydoBalacnce(g);
            findQuellenSenken(g, out List<Knoten> quellen, out List<Knoten> senken);
            double wegDist = 0;
            Graph resi = null;

            //ueber alle Qullen und Senken solange eine erhoehungMoeglich ist
            bool erhoehungMoglich = true;
            while (erhoehungMoglich && quellen.Count > 0 && senken.Count > 0)
            {
                erhoehungMoglich = false;
                for (int i = 0; i < quellen.Count; i++)
                {
                    //doppel Abfrage auf Quellen und Senken notig, da findQuelllen neue Listen erzeugen kann
                    for (int j = 0; j < senken.Count && i < quellen.Count; j++) {
                        resi = g.createResidualGraph();
                        var weg = new List<Knoten>();

                        //wenn wegDist= PositiveInfinity dann gibt es keinen weg
                        wegDist = new MoorBellmanFord().ShortestWay(resi, quellen[i], senken[j], out weg);
                        if (double.PositiveInfinity != wegDist)
                        {
                            erhoehungMoglich |= flussErhoehen(ref g, weg);
                            this.psydoBalance = calcPsydoBalacnce(g);
                            findQuellenSenken(g, out quellen, out senken);
                        }
                    }
                    this.psydoBalance = calcPsydoBalacnce(g);
                    findQuellenSenken(g, out quellen, out senken);
                }
            }
            this.psydoBalance = calcPsydoBalacnce(g);
            findQuellenSenken(g, out quellen, out senken);

            if (quellen.Count > 0 ||senken.Count > 0 || !erhoehungMoglich)
            {
                throw new NotBflussException("Es konnte kein B-Fluss gefunden werden.");
            }
            return calcFlussKosten(ref g.kanten);
        }

        //return true wenn die erhoehung != 0 war
        private bool flussErhoehen(ref Graph g, List<Knoten> weg)
        {
            Knoten quelle = g.Knoten[weg.First<Knoten>().Wert];
            Knoten senke = g.Knoten[weg.Last<Knoten>().Wert];

            //erhoehung inital auf Min aus Quelle oder Senke
            double erhoehung = quelle.Balance - psydoBalance[quelle.Wert];
            double aenderung = psydoBalance[senke.Wert] - senke.Balance;
            if (erhoehung > aenderung)
            {
                erhoehung = aenderung;
            }

            //erhöhung ermitteln
            Knoten von = g.Knoten[weg[0].Wert];
            for (int i = 1; i < weg.Count; i++) {
                aenderung = von.getToKante(g.Knoten[weg[i].Wert]).RestKapazitaet;
                if (erhoehung > aenderung)
                {
                    erhoehung = aenderung;
                }
                von = g.Knoten[weg[i].Wert];
            }

            //wenn erhoehung == 0, muss nicht über die kanten gelaufen werden
            if(erhoehung == 0)
            {
                return false;
            }

            if(erhoehung < 0)
            {
                throw new AlgorithmException("SSP erzeugt eine Negative Aenderung! Das darf nicht passieren");
            }

            von = g.Knoten[weg[0].Wert];
            for (int i = 1; i < weg.Count; i++)
            {
                von.getToKante(g.Knoten[weg[i].Wert]).Fluss += erhoehung;
                von = g.Knoten[weg[i].Wert];
            }

            return true;
        }

        /// <summary>
        /// Setzt bei allen Kanten mit Kosten <0 den Fluss auf den Max Wert
        /// </summary>
        /// <param name="kantenList"></param>
        private void setzteStartFluss(ref List<Kante> kantenList)
        {
            foreach(Kante k in kantenList)
            {
                if(k.Kosten < 0)
                {
                    k.setMaxFluss();
                } else
                {
                    k.Fluss = 0;
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
