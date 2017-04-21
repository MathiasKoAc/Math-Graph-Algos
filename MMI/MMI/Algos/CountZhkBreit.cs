using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class CountZhkBreit : ICountZusammenhangskomp
    {
        public int CountZhk(Graph Gra)
        {
            int tagLevel = 0; //entsprich der anz der ZHK
            foreach (KeyValuePair<int, Knoten> kVP in Gra.Konten)
            {
                if (kVP.Value.Tag == -1)
                {
                    //neuer Knoten in der Liste -> potentieller neuer ZHK
                    if (breit(kVP.Value, tagLevel))
                    {
                        tagLevel++;
                        //Console.WriteLine("Breit Neuer Khz");
                    }
                    else
                    {
                        //Console.WriteLine("Breit Alter Khz");
                    }
                }
            }
            return tagLevel;
        }

        private bool breit(Knoten kn, int tagLv)
        {
            Queue<Knoten> queue = new Queue<Knoten>();
            queue.Enqueue(kn);
            bool neuerZHK = true;
            kn.Tag = tagLv;

            //Mache solange bis Warteschlange größer als 0 ist
            while (queue.Count > 0)
            {
                Knoten knoten = queue.Dequeue();

                foreach (var kante in knoten.Kanten)
                {
                    if (kante.ToKnoten.Tag == -1)
                    {
                        queue.Enqueue(kante.ToKnoten);
                        kante.ToKnoten.Tag = tagLv;
                    }
                    else if (kante.ToKnoten.Tag < tagLv)
                    {
                        neuerZHK = false;
                    }
                }
            }
            
            return neuerZHK;
        }
    }
}
