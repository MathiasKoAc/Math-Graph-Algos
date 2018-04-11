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
        private double distance;
        private Knoten vorgaenger;
        private Kante vorgaengerKante;
        private double balance;

        public Knoten()
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.residualKanten = new List<Kante>();
            this.distance = Double.MaxValue;
        }

        public Knoten(int wert)
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.residualKanten = new List<Kante>();
            this.wert = wert;
            this.distance = Double.MaxValue;
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

        public double Balance
        {
            get
            {
                return this.balance;
            }

            set
            {
                this.balance = value;
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

        public Kante VorgaengerKante
        {
            get
            {
                return this.vorgaengerKante;
            }

            set
            {
                this.vorgaengerKante = value;
            }
        }

        public ref List<Kante> ResidualKanten => ref residualKanten;
        public ref List<Kante> Kanten => ref kanten;
    }
}
