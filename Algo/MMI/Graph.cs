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
