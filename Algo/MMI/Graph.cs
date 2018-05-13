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
        private List<Kante> kanten;
        private List<Knoten> knoten;     //Knoten mit Wert 1 an Stelle 1 usw

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

                Kante neueKante = new Kante(knotenList[kant.FromKnoten.Wert], knotenList[kant.FromKnoten.Wert], kant.Gewicht);
                knotenList[kant.FromKnoten.Wert].Kanten.Add(neueKante);
                knotenList[kant.ToKnoten.Wert].Kanten.Add(neueKante);
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
            kanten = Kanten;
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
            return kanten.Count;
        }

        public ref List<Knoten> Knoten => ref knoten;

        public List<Kante> Kanten
        {
            get
            {
                return kanten;
            }
            set
            {
                kanten = value;
            }
        }

        public Kante findKante(Knoten vonK, Knoten zuK)
        {
            Kante findK = null;
            if(vonK != null && zuK != null)
            {
                foreach (Kante kant in kanten)
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
            foreach (Kante kant in kanten)
            {
                if (kant.ToKnoten.Wert == zuK && kant.FromKnoten.Wert == vonK)
                {
                    findK = kant;
                    break;
                }
            }

            return findK;
        }

        /* ------------- */
        /* -- Methods -- */
        /* ------------- */

        public void resetKnotenTag(int withTag = -1)
        {
            foreach(Knoten k in this.knoten)
            {
                k.Tag = withTag;
            }
        }

        public void resetKantenTag(int withTag = -1)
        {
            foreach (Kante k in this.kanten)
            {
                k.Tag = withTag;
            }
        }
    }
}
