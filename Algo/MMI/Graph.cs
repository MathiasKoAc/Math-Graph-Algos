using MMI.Algos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Graph
    {
        private List<Knoten> knoten;     //Knoten mit Wert 1 an Stelle 1 usw
        public ref List<Knoten> Knoten => ref knoten;

        public List<Kante> kanten;
        public ref List<Kante> Kanten => ref kanten;

        private List<Kante> residualKanten;

        public static Graph createInstance(List<Kante> KantenList)
        {
            List<Knoten> knotenList = new List<Knoten>();
            List<Kante> neueKantenList = new List<Kante>();

            int maxKnoten = 0;
            foreach (Kante kant in KantenList)
            {
                if(kant.ToKnoten.Wert > maxKnoten)
                {
                    maxKnoten = kant.ToKnoten.Wert;
                }

                if (kant.FromKnoten.Wert > maxKnoten)
                {
                    maxKnoten = kant.FromKnoten.Wert;
                }
            }
            
            for(int i = 0; i <= maxKnoten; i++)
            {
                knotenList.Add(new Knoten(i));
            }

            foreach(Kante kant in KantenList)
            {
                if(knotenList[kant.ToKnoten.Wert] == null)
                {
                    Knoten k = new Knoten(kant.ToKnoten.Wert);
                    knotenList.Add(k);
                }
                if (knotenList[kant.FromKnoten.Wert] == null)
                {
                    Knoten k = new Knoten(kant.FromKnoten.Wert);
                    knotenList.Add(k);
                }

                Kante neueKante = new Kante(knotenList[kant.FromKnoten.Wert], knotenList[kant.ToKnoten.Wert], kant.Kosten, kant.Gewicht);
                if(kant.KantenTyp == KantenTyp.StandartKante)
                {
                    neueKante.Fluss = kant.Fluss;
                }
                knotenList[kant.FromKnoten.Wert].Kanten.Add(neueKante);
                neueKantenList.Add(neueKante);
            }

            //sortieren fuer das bevorzugen des kleinen Weges
            foreach(Knoten k in knotenList)
            {
                k.Kanten.Sort();
            }

            return new Graph(neueKantenList, knotenList);
        }

        public Graph (List<Kante> Kanten, List<Knoten> Knoten)
        {
            this.Kanten = Kanten;
            knoten = Knoten;
        }

        /* ------------------------ */
        /* -- GET / SET / Params -- */
        /* ------------------------ */

        public int getAnzKnoten()
        {
            return knoten.Count;
        }

        public int getAnzKanten()
        {
            return Kanten.Count;
        }

        /* ------------- */
        /* -- Methods -- */
        /* ------------- */

        public Graph createResidualGraph()
        {
            createResidualKanten();
            var kantenForNewG = this.kanten.Where(r => r.RestKapazitaet > 0).ToList<Kante>();
            kantenForNewG.AddRange(new List<Kante>(this.residualKanten.Where(k => k.RestKapazitaet > 0).ToList<Kante>()));
            Graph resiGraph = Graph.createInstance(kantenForNewG);
            balancenAnpassen(ref resiGraph);
            return resiGraph;
        }

        public Graph createUnrichteteKopie()
        {
            var listKanten = new List<Kante>();
            
            foreach(Kante k in this.Kanten)
            {
                listKanten.Add(new Kante(k.ToKnoten, k.FromKnoten, k.Kosten, k.Gewicht));
            }

            listKanten.AddRange(this.kanten);
            return createInstance(listKanten);
        }

        private void balancenAnpassen(ref Graph g)
        {
            for(int i = 0; i < this.getAnzKnoten() && i < g.getAnzKnoten(); i++)
            {
                g.Knoten[i].Balance = this.knoten[i].Balance;
            }
        }

        public void createResidualKanten()
        {
            if (this.residualKanten == null)
            {
                this.residualKanten = new List<Kante>();
                foreach (Kante k in Kanten)
                {
                    residualKanten.Add(k.getResidualKante());
                }
            }
        }

        public void setSuperQuelleSenke(List<Knoten> quellen, List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, bool kapaGrenze = false)
        {
            superQuelle = new Knoten(this.Knoten.Count);
            superSenke = new Knoten(this.Knoten.Count + 1);

            Kante tmpKant = null;
            double kapaGrenzwert = double.PositiveInfinity;
            foreach (Knoten q in quellen)
            {

                if(kapaGrenze)
                {
                    kapaGrenzwert = q.Balance;
                }
                tmpKant = new Kante(superQuelle, q, kapaGrenzwert);
                superQuelle.AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }
            this.Knoten.Add(superQuelle);

            kapaGrenzwert = double.PositiveInfinity;
            for (int i = 0; i < senken.Count; i++)
            {
                if(kapaGrenze)
                {
                    kapaGrenzwert = -1 * senken[i].Balance;
                }
                tmpKant = new Kante(senken[i], superSenke, kapaGrenzwert);
                this.Knoten[senken[i].Wert].AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }
            this.Knoten.Add(superSenke);
        }

        public void setSuperQuelleSenke(List<Knoten> quellen, List<Knoten> senken, out Knoten superQuelle, out Knoten superSenke, int kapaGrenzwert)
        {
            superQuelle = new Knoten(this.Knoten.Count);
            superSenke = new Knoten(this.Knoten.Count + 1);

            Kante tmpKant = null;
            foreach (Knoten q in quellen)
            {
                tmpKant = new Kante(superQuelle, q, kapaGrenzwert);
                superQuelle.AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }
            this.Knoten.Add(superQuelle);

            for (int i = 0; i < senken.Count; i++)
            {
                tmpKant = new Kante(senken[i], superSenke, kapaGrenzwert);
                this.Knoten[senken[i].Wert].AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }
            this.Knoten.Add(superSenke);
        }

        public void addSuperQuelleSenke(List<Knoten> additionalQuellen, List<Knoten> additionalSenken, ref Knoten superQuelle, ref Knoten superSenke, bool kapaGrenze = false)
        {
            Kante tmpKant = null;
            double kapaGrenzwert = double.PositiveInfinity;
            foreach (Knoten q in additionalQuellen)
            {
                if (kapaGrenze)
                {
                    kapaGrenzwert = q.Balance;
                }
                tmpKant = new Kante(superQuelle, q, kapaGrenzwert);
                superQuelle.AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }

            kapaGrenzwert = double.PositiveInfinity;
            for (int i = 0; i < additionalSenken.Count; i++)
            {
                if (kapaGrenze)
                {
                    kapaGrenzwert = -1 * additionalSenken[i].Balance;
                }
                tmpKant = new Kante(additionalSenken[i], superSenke, kapaGrenzwert);
                this.Knoten[additionalSenken[i].Wert].AddKante(tmpKant);
                this.Kanten.Add(tmpKant);
            }
        }

        public void delSuperQuelle(Knoten superQuelle)
        {
            //Kanten von SuperQuelle aus löschen
            foreach (Kante q in superQuelle.Kanten)
            {
                this.Kanten.Remove(q);
            }

            //Super SuperQuelle aus g entfernen
            this.Knoten.Remove(superQuelle);

            residualKanten.RemoveAll(kant => kant.ToKnoten.Wert == superQuelle.Wert);

            //Super Quellen-Kanten aus ResiKanten löschen
            residualKanten.RemoveAll(kant => kant.ToKnoten.Wert == superQuelle.Wert);
        }

        public void delSuperSenke(List<Knoten> senken, Knoten superSenke)
        {
            residualKanten.RemoveAll(kant => kant.FromKnoten.Wert == superSenke.Wert);

            //Kanten zur SuperSenke löschen
            foreach (Knoten k in senken)
            {
                if (k.Kanten != null)
                {
                    k.Kanten.RemoveAll(kant => kant.ToKnoten.Wert == superSenke.Wert);
                }
            }
            //Super Senke aus g entfernen
            this.Knoten.Remove(superSenke);

            //Super Senken-Kanten aus Kanten löschen
            Kanten.RemoveAll(kant => kant.ToKnoten.Wert == superSenke.Wert);
        }

        public double findSmallesKantenGewicht(out Kante kante)
        {
            double smalles = Double.MaxValue;
            kante = Kanten[0];

            foreach(Kante kn in Kanten)
            {
                if(kn.Gewicht < smalles)
                {
                    smalles = kn.Gewicht;
                    kante = kn;
                }
            }
            return smalles;
        }

        public Kante findKante(Knoten vonK, Knoten zuK)
        {
            Kante findK = null;
            if(vonK != null && zuK != null)
            {
                findK = this.findKante(vonK.Wert, zuK.Wert);
            }

            return findK;
        }

        public Kante findKante(int vonK, int zuK)
        {
            Kante findK = null;
            foreach (Kante kant in Kanten)
            {
                if (kant.ToKnoten.Wert == zuK && kant.FromKnoten.Wert == vonK)
                {
                    findK = kant;
                    break;
                }
            }

            return findK;
        }

        public void resetKnotenTag(int withTag = -1)
        {
            foreach(Knoten k in this.knoten)
            {
                k.Tag = withTag;
            }
        }

        public void resetKantenTag(int withTag = -1)
        {
            foreach (Kante k in this.Kanten)
            {
                k.Tag = withTag;
            }
        }

        public void resetFluss(double fluss = 0)
        {
            foreach (Kante k in this.Kanten)
            {
                k.Fluss = fluss;
            }
        }
    }
}
