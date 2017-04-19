using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Knoten
    {
        private int wert;
        private List<Kante> kanten;
        private Tag tag;

        public Knoten()
        {
            this.tag = new MMI.Tag();
            this.kanten = new List<Kante>();
        }

        public Knoten(int wert)
        {
            this.tag = new MMI.Tag();
            this.kanten = new List<Kante>();
            this.wert = wert;
        }

        public Knoten(Tag tag)
        {
            this.tag = tag;
            this.kanten = new List<Kante>();
        }

        public Knoten(int wert, Tag tag)
        {
            this.tag = tag;
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

        public Tag Tag
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
