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
        
        public bool IsLeer()
        {
            return (erster == null);
        }

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
                FastLink preRunner = null;
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
                    preRunner = runner;
                    runner = runner.Back;
                }

                if(!added && preRunner != null)
                {
                    FastLink f = new FastLink(k);
                    preRunner.Back = f;
                    f.Front = preRunner;
                }
            }
        }

        public Kante PullMin()
        {
            FastLink tmp = erster;
            erster = tmp.Back;
            tmp.Back = null;
            if (erster != null)
            {
                erster.Front = null;
            }
            
            return tmp.K;
        }
    }
}
