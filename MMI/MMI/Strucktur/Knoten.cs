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
        private int tag;
        private double distance;
        private Knoten vorgaenger;

        public Knoten()
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.distance = Double.MaxValue;
        }

        public Knoten(int wert)
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.wert = wert;
            this.distance = Double.MaxValue;
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

        public double Distance
        {
            get
            {
                return this.distance;
            }

            set
            {
                this.distance = value;
            }
        }

        public Knoten Vorgaenger
        {
            get
            {
                return this.vorgaenger;
            }

            set
            {
                this.vorgaenger = value;
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
