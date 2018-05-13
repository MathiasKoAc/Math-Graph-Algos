using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Knoten
    {
        private int wert;       //entspricht dem Namen
        private List<Kante> kanten;
        private List<Kante> residualKanten;
        private int tag;

        public Knoten()
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.residualKanten = new List<Kante>();
        }

        public Knoten(int wert)
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.residualKanten = new List<Kante>();
            this.wert = wert;
        }

        public void AddKante(Kante k)
        {
            kanten.Add(k);
        }

        public void AddResidualKante(Kante k)
        {
            residualKanten.Add(k);
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

        public ref List<Kante> ResidualKanten => ref residualKanten;
        public ref List<Kante> Kanten => ref kanten;


        public override string ToString()
        {
            return "K: " + this.Wert;
        }
    }
}
