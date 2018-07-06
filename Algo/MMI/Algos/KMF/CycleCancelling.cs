using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CycleCancelling : AbsKMF
    {
        private List<List<Knoten>> zhkList;

        public double calcKMF(Graph g)
        {
            int zhkCount = new CountZhkBreit().CountZhk(g.createUnrichteteKopie(), out this.zhkList);

            double initalKosten = calcInitalBfluss(ref g);

            Graph resiGra;
            bool zyclus = true;
            while (zyclus)
            {
                resiGra = g.createResidualGraph();
                zyclus = findNegativCycle(ref resiGra, ref g);
            }

            if(!this.ausgeglichenerBFluss(ref g))
            {
                throw new NotBflussException("Es konnte kein B-Fluss gefunden werden.");
            }

            return this.calcFlussKosten(ref g.kanten);
        }

        //true wenn Cycle gefunden
        private bool findNegativCycle(ref Graph resiGra, ref Graph g)
        {
            setupSuperQullenSenkeMitAllenZhk(ref resiGra, out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, true);

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
                double tmpAnpassung = double.MaxValue;
                List<Kante> kantenForAnderung = new List<Kante>();
                bool[] isResi = new bool[wayTree.Count];

                for (int i = 0; i < wayTree.Count && !checkArray[fokusKnoten.Wert]; i++)
                {

                    checkArray[fokusKnoten.Wert] = true;
                    Kante kant = g.findKante(wayTree[fokusKnoten.Wert].VorgangerKnoten, fokusKnoten);
                    if (kant != null)
                    {
                        tmpAnpassung = kant.RestKapazitaet;
                        //hier ist die Kante normal gerichtet
                        isResi[i] = false;
                    } else
                    {
                        //hier ist die Kante residual gerichtet
                        kant = g.findKante(fokusKnoten, wayTree[fokusKnoten.Wert].VorgangerKnoten);
                        tmpAnpassung = kant.RestKapazitaet;
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
            setupSuperQullenSenke(ref g, out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, true);
            new EdmondsKarp().calcMFP(g, superQuelle, superSenke);

            g.delSuperSenke(senken, superSenke);
            g.delSuperQuelle(superQuelle);

            return 0d;
            
        }

        protected bool ausgeglichenerBFluss (ref Graph g)
        {
            double[] psydoBalance = this.calcPsydoBalacnce(g);
            for(int i = 0; i < g.Knoten.Count; i++)
            {
                if(psydoBalance[i] != g.Knoten[i].Balance)
                {
                    return false;
                }
            }
            return true;
        }

        public void setupSuperQullenSenke(ref Graph g, out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, bool kapaGrenze = false)
        {
            senken = new List<Knoten>();
            quellen = new List<Knoten>();

            foreach (Knoten k in g.Knoten)
            {
                if (k.Balance > 0)
                {
                    quellen.Add(k);
                }
                else if (k.Balance < 0)
                {
                    senken.Add(k);
                }
            }

            g.setSuperQuelleSenke(quellen, senken, out superQuelle, out superSenke, kapaGrenze);
        }

        public void setupSuperQullenSenkeMitAllenZhk(ref Graph g, out List<Knoten> quellen, out List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, bool kapaGrenze = false)
        {
            if (this.zhkList == null)
            {
                throw new Exception("Zhl List fehlt!");
            }

            senken = new List<Knoten>();
            quellen = new List<Knoten>();
            var additionalSenken = new List<Knoten>();
            var additionalQuellen = new List<Knoten>();

            int zhkCount = zhkList.Count;
            bool[] checkQuellen = new bool[zhkCount];
            bool[] checkSenken = new bool[zhkCount];


            for (int i = 0; i < zhkCount; i++)
            {
                foreach (Knoten k in zhkList[i])
                {
                    if (k.Balance > 0)
                    {
                        quellen.Add(k);
                        checkQuellen[i] = true;
                    }
                    else if (k.Balance < 0)
                    {
                        senken.Add(k);
                        checkSenken[i] = true;
                    }
                }

                if (!checkQuellen[i])
                {
                    additionalQuellen.Add(zhkList[i].First<Knoten>());
                }
                if (!checkSenken[i])
                {
                    additionalSenken.Add(zhkList[i].Last<Knoten>());
                }
            }

            g.setSuperQuelleSenke(quellen, senken, out superQuelle, out superSenke, kapaGrenze);
            g.addSuperQuelleSenke(additionalQuellen, additionalSenken, ref superQuelle, ref superSenke);

            quellen.AddRange(additionalQuellen);
            senken.AddRange(additionalSenken);
        }
    }
}
