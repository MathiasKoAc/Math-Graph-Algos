using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class Knoten
    {
        private int wert;
        private List<Kante> kanten;

        public Knoten()
        {
            this.kanten = new List<Kante>();
        }

        public Knoten(int wert)
        {
            this.kanten = new List<Kante>();
            this.wert = wert;
        }

        public void AddKante(Kante k)
        {
            kanten.Add(k);
        }

        public int Wert
        {
            get
            {
                return wert;
            }
        }

        public List<Kante> Kanten
        {
            get
            {
                return kanten;
            }
        }
        
    }
}
