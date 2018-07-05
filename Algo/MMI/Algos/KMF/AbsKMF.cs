using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    abstract public class AbsKMF
    {

        //TODO in SUPER Klasse
        protected double calcFlussKosten(ref List<Kante> kanten)
        {
            double sum = 0;
            foreach (Kante k in kanten)
            {
                sum += (k.Fluss * k.Kosten);
            }
            return sum;
        }
    }
}
