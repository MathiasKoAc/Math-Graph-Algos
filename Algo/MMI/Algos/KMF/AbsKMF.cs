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

        protected double[] calcPsydoBalacnce(Graph g)
        {
            // Knoten muss einem Ausfluss - Einfluss > 0 sind positiv
            double[] d = new double[g.Knoten.Count];

            List<Knoten> knotenList = g.Knoten;
            for (int i = 0; i < g.Knoten.Count; i++)
            {
                d[g.Knoten[i].Wert] = g.Knoten[i].calcAusfluss(false);
            }

            for (int i = 0; i < g.Kanten.Count; i++)
            {
                d[g.Kanten[i].ToKnoten.Wert] -= g.Kanten[i].Fluss;
            }
            return d;
        }
    }
}
