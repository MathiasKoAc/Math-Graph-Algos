using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI_SI
{
    public class Kante
    {
        private int _gewicht = 0;
        private Knoten toKnoten;

        public Kante(Knoten toK, int gewicht)
        {

            toKnoten = toK;
            _gewicht = gewicht;
        }

        public Kante(Knoten fromK, Knoten toK)
        {
            toKnoten = toK;
        }

        public Knoten ToKnoten
        {
            get
            {
                return toKnoten;
            }
        }
    }
}
