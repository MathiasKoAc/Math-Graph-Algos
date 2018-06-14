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

        public ref List<Kante> Kanten => ref kanten;

        public Kante getToKante(Knoten toKnoten)
        {
            foreach(Kante kn in kanten)
            {
                if(kn.ToKnoten.wert == toKnoten.wert)
                {
                    return kn;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "K: " + this.Wert;
        }
    }
}
