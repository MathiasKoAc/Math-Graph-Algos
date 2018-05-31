using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class Dijkstra
    {
        public double ShortestWay(Graph g, Knoten StartKnoten, Knoten ZielKnoten, out List<Knoten> weg)
        {
            ShortestWayTree(g, StartKnoten, out List<DijKnoten> dijKnotenMap);
            weg = new List<Knoten>();
            
            DijKnoten fokusDij = dijKnotenMap[ZielKnoten.Wert];
            double ret = fokusDij.Distanze;
            weg.Add(fokusDij.HauptKnoten);
            /*while (fokusDij.HauptKnoten.Wert != fokusDij.VorgangerKnoten.Wert)
            {
                fokusDij = dijKnotenMap[fokusDij.VorgangerKnoten.Wert];
                weg.Add(fokusDij.HauptKnoten);
            }*/
            return ret;
        }

        public void ShortestWayTree(Graph g, Knoten StartKnoten, out List<DijKnoten> dijKnotenList)
        {
            HashSet<DijKnoten> sortedKnoten = createKnotenSet(g, ref StartKnoten, out dijKnotenList);

            DijKnoten dij = null;
            DijKnoten nachfolger = null;
            double dist = Double.PositiveInfinity;

            while ((dij = sortedKnoten.Min<DijKnoten>()) != null && dij.Distanze < Double.MaxValue)
            {
                foreach (Kante kant in dij.HauptKnoten.Kanten)
                {
                    nachfolger = dijKnotenList[kant.ToKnoten.Wert];
                    if (dij.HauptKnoten.Tag != 1)
                    {
                        dist = dij.Distanze + kant.Gewicht;
                        if (dist < nachfolger.Distanze)
                        {
                            nachfolger.VorgangerKnoten = dij.HauptKnoten;
                            nachfolger.Distanze = dist;
                        }
                    }
                }
                sortedKnoten.Remove(dij);
                dij.HauptKnoten.Tag = 1;
            }
        }

        private HashSet<DijKnoten> createKnotenSet(Graph g, ref Knoten StartKnoten, out List<DijKnoten> dijKnotenList)
        {
            HashSet<DijKnoten> sortedKnoten = new HashSet<DijKnoten>();
            dijKnotenList = new List<DijKnoten>();

            DijKnoten dij = null;
            foreach (Knoten kn in g.Knoten)
            {
                if(kn.Wert == StartKnoten.Wert)
                {
                    dij = addKnoten(kn, kn, 0.0, ref sortedKnoten);
                    dijKnotenList.Add(dij);
                } else
                {
                    dij = addKnoten(kn, null, double.MaxValue, ref sortedKnoten);
                    dijKnotenList.Add(dij);
                }
                
            }
            return sortedKnoten;
        }

        private DijKnoten addKnoten(Knoten K, Knoten vorgangerKn, double distanze, ref HashSet<DijKnoten> sortedKn)
        {
            DijKnoten dij = new DijKnoten(K, vorgangerKn, distanze);
            sortedKn.Add(dij);
            return dij;
        }
        
    }
}
