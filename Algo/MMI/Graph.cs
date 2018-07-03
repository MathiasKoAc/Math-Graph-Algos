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
            return Graph.createInstance(kantenForNewG);
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
                foreach (Kante kant in Kanten)
                {
                    if (kant.ToKnoten == zuK && kant.FromKnoten == vonK)
                    {
                        findK = kant;
                        break;
                    }
                }
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
