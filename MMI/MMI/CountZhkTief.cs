using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class CountZhkTief : ICountZusammenhangskomp
    {
        public int CountZhk(Graph Gra)
        {
            return CountZhk(Gra, Gra.Kanten[0].ToKnoten);
        }

        public int CountZhk(Graph Gra, Knoten StartKn)
        {
            int tagLevel = 0;
            StartKn.Tag = tagLevel;
            foreach(KeyValuePair<int, Knoten> kVP in Gra.Konten) {
                if(!(kVP.Value.Tag >= 0))
                {

                }
            }

            throw new NotImplementedException();
        }

        private void deep(Knoten kn, int tagLv)
        {
            kn.Tag = tagLv;
            Knoten toKn;
            foreach(Kante kant in kn.Kanten) {
                toKn = kant.ToKnoten;
                if (!(toKn.Tag >= 0))
                {
                    deep(kant.ToKnoten, tagLv);
                }                
            }
        }
    }
}
