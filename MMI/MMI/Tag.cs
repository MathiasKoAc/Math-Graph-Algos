using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Tag
    {
        private int wert;

        public Tag()
        {
            wert = -1;
        }

        public Tag(int wert)
        {
            this.wert = wert;
        }

        public int Wert {
            get
            {
                return this.wert;
            }

            set
            {
                this.wert = value;
            }
        }

    }
}
