using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI_SI
{
    public class Knoten
    {
        private int wert;
		private List<Kante> kanten;
        private int tag;

        public Knoten()
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
        }

        public Knoten(int wert)
        {
            this.tag = -1;
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

        public int Tag
        {
            get
            {
                return tag;
            }

            set
            {
                tag = value;
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
