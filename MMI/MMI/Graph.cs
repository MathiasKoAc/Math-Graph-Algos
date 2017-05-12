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
        private Dictionary<int, Knoten> knoten;

        public Graph (List<Kante> Kanten, Dictionary<int, Knoten> Knoten)
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

        public Dictionary<int, Knoten> Knoten{
            get {
                return knoten;
            }
            set {
                knoten = value;
            }
        }

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

        public void resetKnotenTag()
        {
            foreach(KeyValuePair<int, Knoten> pair in this.knoten)
            {
                pair.Value.Tag = -1;
            }
        }

        public int countZhk(ICountZusammenhangskomp cZhk)
        {
            return cZhk.CountZhk(this);
        }

        public double countMST(ICountMST obj)
        {
            List<Kante> mKanten;
            return obj.CountMST(this, out mKanten);
        }

        public double countTSPTripp(ICountTSP cTsp, bool show)
        {
            List<Knoten> Knotens;
            double count = cTsp.roundTripp(this, this.Knoten[0], out Knotens);
            if(show)
            {
                Console.WriteLine("\n----");
                foreach (Knoten k in Knotens)
                {
                    Console.WriteLine("#" + k.Wert);
                }
            }            
            return count;
        }
    }
}
