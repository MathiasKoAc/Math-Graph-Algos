using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class InsertSortList
    {
        private FastLink erster;
        
        public void Add(Kante k)
        {
            if(erster == null)
            {
                erster = new FastLink(k);
            }
            else
            {
                bool added = false;
                FastLink runner = erster;
                while(runner != null && !added)
                {
                    if(runner.K.Gewicht >= k.Gewicht)
                    {
                        if(runner.Front == null)
                        {
                            FastLink f = new FastLink(k, runner);
                            erster = f;
                            added = true;
                        } else
                        {
                            FastLink f = new FastLink(k, runner.Front, runner);
                            added = true;
                        }
                        
                    }
                    runner = runner.Back;
                }
            }
        }

        public Kante PullMin()
        {
            FastLink tmp = erster;
            erster = tmp.Back;
            erster.Front = null;
            tmp.Back = null;
            return tmp.K;
        }
    }
}
