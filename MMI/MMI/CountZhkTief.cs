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
            int tagLevel = 0; //entsprich der anz der ZHK
            List<Tag> tags = new List<Tag>();
            Tag aktTag = new Tag(0);
            tags.Add(aktTag);
            StartKn.Tag = aktTag;
            foreach(KeyValuePair<int, Knoten> kVP in Gra.Konten) {
                if (kVP.Value.Tag.Wert == -1)
                {
                    //neuer Knoten in der Liste -> potentieller neuer ZHK
                    if (deep(kVP.Value, tagLevel))
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
                }else if(toKn.Tag < tagLv)
                {
                    //Tag > -1 und Tag < tagLv also Knoten aus altem ZHK
                    neuerZHK = false;
                }                
            }
            return neuerZHK;
        }
    }
}
