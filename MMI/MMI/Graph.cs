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

        public Dictionary<int, Knoten> Konten{
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

            if (findK == null)
            {
                Console.WriteLine(vonK.Wert + " -> " + zuK.Wert + " = null");
            } else
            {
                Console.WriteLine(vonK.Wert + " -> " + zuK.Wert + " != null");
            }
            return findK;
        }

        /* ------------- */
        /* -- Methods -- */
        /* ------------- */

        public int countZhk(ICountZusammenhangskomp cZhk)
        {
            return cZhk.CountZhk(this);
        }

        public double countMST(ICountMST obj)
        {
            List<Kante> mKanten;
            return obj.CountMST(this, out mKanten);
        }

        public double countTSPTripp(ICountTSP cTsp)
        {
            List<Knoten> Knotens;
            double count = cTsp.roundTripp(this, this.Konten[0], out Knotens);
            foreach(Knoten k in Knotens)
            {
                Console.WriteLine("#" + k.Wert);
            }
            return count;
        }
    }
}
