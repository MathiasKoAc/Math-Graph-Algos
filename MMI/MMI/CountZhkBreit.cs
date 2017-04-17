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
            
        }

        private bool breit(Knoten kn, int tagLv)
        {
            Queue<Knoten> queue = new Queue<Knoten>();
            queue.Enqueue(kn);

            //Mache solange bis Warteschlange größer als 0 ist
            while (queue.Count > 0)
            {
                Knoten knoten = queue.Dequeue();

                if (knoten.Tag == -1)
                {
                    knoten.Tag = tagLv;

                    foreach (var kante in kn.Kanten)
                    {
                        if (knoten.Tag == -1)
                        {
                            queue.Enqueue(kante.ToKnoten);
                        }                            
                    }
                }
            }
            ///___________________________
            return false;
        }
    }
}
