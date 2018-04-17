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
        private double kosten;
        private Knoten toKnoten;
        private Knoten fromKnoten;
        private int tag;
        private double fluss;
        private bool isResidualKante;
        private Kante resiKante;

        public Kante(Knoten fromK, Knoten toK, double kapaziteat, double flusss, bool residual, Kante resiKant)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            this.gewicht = kapaziteat;
            tag = -1;

            this.fluss = flusss;
            isResidualKante = residual;

            //Doppel Referenz in und rückrichtung
            //Die ResiKante der ResiKante ist die OriginalKante
            resiKante = resiKant;
            resiKante.ResiKante = this;
        }

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

        public bool IsResidualKante
        {
            get
            {
                return isResidualKante;
            }
        }

        public double Fluss
        {
            get
            {
                return fluss;
            }
            set
            {
                fluss = value;
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

        public double Kosten
        {
            get
            {
                return kosten;
            }
            set
            {
                kosten = value;
            }
        }

        public Kante ResiKante
        {
            get
            {
                return resiKante;
            }
            set
            {
                resiKante = value;
            }
        }

        public double RestKapazitaet
        {
            get
            {
                //Kapazitaet - Fluss
                return gewicht - fluss;
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
                return gewicht.CompareTo(comparePart.Gewicht);
            }
        }

        public override string ToString()
        {
            return fromKnoten.Wert + " -> " + toKnoten.Wert + " # " + gewicht;
        }
    }
}
