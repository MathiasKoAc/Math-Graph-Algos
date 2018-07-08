using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Knoten : IComparable<Knoten>
    {
        private int wert;       //entspricht dem Namen
        private List<Kante> kanten;
        private int tag;
        private double balance;

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

        public Knoten(int wert, double balance)
        {
            this.tag = -1;
            this.kanten = new List<Kante>();
            this.wert = wert;
            this.balance = balance;
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

        public double Balance { get => balance; set => balance = value; }

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

        public double calcAusfluss(bool alle = true)
        {
            double ausFluss = 0d;
            foreach(Kante kant in Kanten)
            {
                if(alle || kant.KantenTyp == KantenTyp.StandartKante)
                {
                    ausFluss += kant.Fluss;
                }
            }
            return ausFluss;
        }

        public int CompareTo(Knoten other)
        {
            return this.Wert - other.Wert;
        }
    }
}
