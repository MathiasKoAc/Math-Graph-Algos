using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI.Algos
{
    class MoorBellmanFord
    {
        public double ShortestWay(Graph g, Knoten StartKnoten, Knoten ZielKnoten, out List<Knoten> weg)
        {
            bool zykelfrei = ShortestWayTree(g, StartKnoten, out List<DijKnoten> dijKnotenMap, out Kante ex);
            weg = new List<Knoten>();

            if (zykelfrei)
            {
                DijKnoten fokusDij = dijKnotenMap[ZielKnoten.Wert];
                double ret = fokusDij.Distanze;
                weg.Add(fokusDij.HauptKnoten);
                while (fokusDij.VorgangerKnoten != null && fokusDij.HauptKnoten.Wert != fokusDij.VorgangerKnoten.Wert)
                {
                    fokusDij = dijKnotenMap[fokusDij.VorgangerKnoten.Wert];
                    weg.Add(fokusDij.HauptKnoten);
                }
                
                if(fokusDij.VorgangerKnoten == null)
                {
                    //kein Wege gefunden
                    weg = null;
                    return double.PositiveInfinity;
                }
                weg.Reverse();
                return ret;
            }
            else
            {
                throw new NegativCycleExeption("Negativen Cycle found");
                //return double.MinValue;
            }
            
        }

        //return false bei negativem Zykel
        public bool ShortestWayTree(Graph g, Knoten StartKnoten, out List<DijKnoten> dijKnotenList, out Kante excetionKante)
        {
            HashSet<DijKnoten> sortedKnoten = createKnotenSet(g, ref StartKnoten, out dijKnotenList);

            int n = g.Knoten.Count;
            for(int i = 0; i < n-1; i++)
            {
                foreach(Kante kant in g.Kanten)
                {
                    if(dijKnotenList[kant.FromKnoten.Wert].Distanze + kant.Kosten < dijKnotenList[kant.ToKnoten.Wert].Distanze)
                    {
                        dijKnotenList[kant.ToKnoten.Wert].Distanze = dijKnotenList[kant.FromKnoten.Wert].Distanze + kant.Kosten;
                        dijKnotenList[kant.ToKnoten.Wert].VorgangerKnoten = kant.FromKnoten;
                    }
                }
            }

            foreach (Kante kant in g.Kanten)
            {
                if (dijKnotenList[kant.FromKnoten.Wert].Distanze + kant.Kosten < dijKnotenList[kant.ToKnoten.Wert].Distanze)
                {
                    excetionKante = kant;
                    return false;
                }
            }
            excetionKante = null;
            return true;
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
