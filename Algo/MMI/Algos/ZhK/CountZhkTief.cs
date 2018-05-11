using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CountZhkTief : ICountZusammenhangskomp
    {
        public int CountZhk(Graph Gra)
        {
            return CountZhk(Gra, Gra.Kanten[0].ToKnoten);
        }

        public int CountZhk(Graph Gra, Knoten StartKn)
        {
            Gra.resetKantenTag();
            Gra.resetKnotenTag();

            int tagLevel = 0; //entsprich der anz der ZHK
            StartKn.Tag = tagLevel;
            foreach(Knoten knoten in Gra.Knoten) {
                if (knoten.Tag == -1)
                {
                    //neuer Knoten in der Liste -> potentieller neuer ZHK
                    if (deep(knoten, tagLevel))
                    {
                        //deep == true also neues ZHK
                        tagLevel++;
                    }
                }
            }
            return tagLevel;
        }

        private bool deep(Knoten kn, int tagLv)
        {
            bool neuerZHK = true;
            kn.Tag = tagLv;
            Knoten toKn;
            foreach(Kante kant in kn.Kanten) {
                toKn = kant.ToKnoten;
                if (toKn.Tag == -1)
                {
                    //Tag -1 also neuer Knoten
                    neuerZHK &= deep(kant.ToKnoten, tagLv);
                }else if(toKn.Tag < tagLv)  //checke ob überflüssig
                {
                    //Tag > -1 und Tag < tagLv also Knoten aus altem ZHK
                    neuerZHK = false;
                }                
            }
            return neuerZHK;
        }

        private void resetGraph(Graph Gra)
        {
            foreach (Kante kant in Gra.Kanten)
            {
                kant.Tag = -1;
            }

            foreach (Knoten kont in Gra.Knoten)
            {
                kont.Tag = -1;
            }
        }
    }
}
