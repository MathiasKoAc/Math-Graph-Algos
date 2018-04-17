using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Kruskal : AbsMST
    {
        public override double CountMST(Graph Gra, out List<Kante> Kanten)
        {
            int maxTag = 0;
            Kanten = new List<Kante>();
            double mstSize = 0;

            foreach(Kante k in Gra.Kanten)
            {
                mstSize += addKante(k, ref Kanten, ref maxTag);
            }
            return mstSize;
        }        
    }
}
