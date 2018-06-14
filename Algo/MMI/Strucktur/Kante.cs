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
        private double fluss = 0d;
        private readonly KantenTyp KantenTyp = KantenTyp.StandartKante;
        private Kante residualKante;

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

        public Kante(Knoten fromK, Knoten toK, double gewicht, KantenTyp typ)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            this.gewicht = gewicht;
            tag = -1;
            KantenTyp = typ;
        }

        public Kante(Knoten fromK, Knoten toK, KantenTyp typ)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            tag = -1;
            KantenTyp = typ;
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
                if (this.KantenTyp == KantenTyp.StandartKante)
                {
                    throw new StruckturException("Fehler! Versucht die Kapazität zu ändern. Kapazitäten von StandardKanten sind fest!");
                }
                gewicht = value;
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
                if(this.KantenTyp == KantenTyp.ResidualKante)
                {
                    throw new StruckturException("Fehler! Versuch den Fluss eine ResidualKante zu ändern. Der Fluss einer ResidualKante ist immer 0!");
                }
                fluss = value;
            }
        }

        public double RestKapazitaet
        {
            get
            {
                return Kapazitaet - fluss;
            }
        }

        public Kante ResidualKante
        {
            get
            {
                if(this.residualKante == null)
                {
                    // ResidualKante zeigt in die gegengesetzte Richtung und hat die Kapazität des Flusses der Originalkante
                    residualKante = new Kante(this.toKnoten, this.FromKnoten, this.fluss, KantenTyp.ResidualKante);
                    toKnoten.AddKante(residualKante);
                }
                return this.residualKante;
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
