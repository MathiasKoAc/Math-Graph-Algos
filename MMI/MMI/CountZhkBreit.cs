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
            return CountZhk(Gra, Gra.Kanten[0].ToKnoten);
        }

        public int CountZhk(Graph Gra, Knoten StartKn)
        {
            int tagLevel = 0; //entsprich der anz der ZHK
            StartKn.Tag = tagLevel;
            foreach (KeyValuePair<int, Knoten> kVP in Gra.Konten)
            {
                if (kVP.Value.Tag == -1)
                {
                    //neuer Knoten in der Liste -> potentieller neuer ZHK
                    if (breit(kVP.Value, tagLevel))
                    {
                        //deep == true also neues ZHK
                        tagLevel++;
                        Console.WriteLine("Breit Neuer Khz");
                    } else
                    {
                        Console.WriteLine("Breit Alter Khz");
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

            //Mache solange bis Warteschlange größer als 0 ist
            while (queue.Count > 0)
            {
                Knoten knoten = queue.Dequeue();

                if (knoten.Tag == -1)
                {
                    knoten.Tag = tagLv;
                    Console.WriteLine("Knoten: " + knoten.Wert);
                    foreach (var kante in kn.Kanten)
                    {
                        queue.Enqueue(kante.ToKnoten);                 
                    }
                } else if (knoten.Tag < tagLv)
                {
                    neuerZHK = false;
                }
                //Console.WriteLine(queue.Count);
            }
            
            return neuerZHK;
        }
    }
}
