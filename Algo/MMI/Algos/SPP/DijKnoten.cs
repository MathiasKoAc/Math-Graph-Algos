using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    public class DijKnoten: IComparable<DijKnoten>
    {
        public DijKnoten(Knoten hauptKnoten, Knoten vorgangerKnoten, double distanze)
        {
            this.HauptKnoten = hauptKnoten;
            this.VorgangerKnoten = vorgangerKnoten;
            this.Distanze = distanze;
        }

        public Knoten HauptKnoten;
        public Knoten VorgangerKnoten;
        public double Distanze;

        public int CompareTo(DijKnoten other)
        {
            if(other == null)
            {
                return 1;
            }
            else if ( this.Distanze > other.Distanze )
            {
                return 1;
            }
            else
            {
                return -1;
            }
            
        }
    }
}
