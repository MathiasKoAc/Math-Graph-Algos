using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI_SI
{
    public class Knoten
    {
        private int wert;
        private int tag;

        public Knoten()
        {
            this.tag = -1;
        }

        public Knoten(int wert)
        {
            this.tag = -1;
            this.wert = wert;
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
    }
}
