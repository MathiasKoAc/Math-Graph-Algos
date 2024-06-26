﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class CountZhkBreit : ICountZusammenhangskomp
    {
        public int CountZhk(Graph Gra, out List<List<Knoten>> zhks)
        {
            int count = CountZhk(Gra);

            zhks = new List<List<Knoten>>();

            for(int i = 0; i < count; i++)
            {
                zhks.Add(new List<Knoten>());
            }

            foreach(Knoten kno in Gra.Knoten)
            {
                zhks[kno.Tag].Add(kno);
            }
            return count;
        }

        public int CountZhk(Graph Gra)
        {
            Gra.resetKantenTag();
            Gra.resetKnotenTag();

            DateTime dTime = DateTime.Now;
            long t1 = dTime.Millisecond;
            int tagLevel = 0; //entsprich der anz der ZHK
            foreach (Knoten kVP in Gra.Knoten)
            {
                if (kVP.Tag == -1)
                {
                    //neuer Knoten in der Liste -> potentieller neuer ZHK
                    if (breit(kVP, tagLevel))
                    {
                        tagLevel++;
                        //Console.WriteLine("Breit Neuer Khz");
                    }
                }
            }

            long tend = dTime.Millisecond;
            return (tagLevel);
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
