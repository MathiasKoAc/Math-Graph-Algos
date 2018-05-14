using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Kante : IComparable<Kante>
    {
        private double gewicht; //oder auch kapazitaet
        public double Offset = 0d;
        private Knoten toKnoten;
        private Knoten fromKnoten;
        private int tag;

        public Kante(Knoten fromK, Knoten toK, double gewicht)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            this.gewicht = gewicht;
            tag = -1;
        }

        public Kante(Knoten fromK, Knoten toK)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            tag = -1;
        }

        public Knoten ToKnoten
        {
            get
            {
                return toKnoten;
            }
        }

        public Knoten FromKnoten
        {
            get
            {
                return fromKnoten;
            }
        }

        public double Gewicht
        {
            get
            {
                return gewicht;
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

        public double Kapazitaet
        {
            get
            {
                return gewicht;
            }
            set
            {
                gewicht = value;
            }
        }

        public int CompareTo(Kante comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
            {
                return 1;
            }
            /*else if (comparePart.Gewicht == _gewicht)
            {
                return 1;
            }*/
            else
            {  
                //TODO kleiner gleich?
                return (gewicht + Offset).CompareTo((comparePart.Gewicht + comparePart.Offset));
            }
        }

        public override string ToString()
        {
         
   return fromKnoten.Wert + " -> " + toKnoten.Wert + " # " + gewicht + " off:"+Offset;
        }
    }
}
