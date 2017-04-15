using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI_SI
{
    public class Graph
    {

		public Dictionary<Knoten, HashSet<Kante>> graphMap  { get; }  = new Dictionary<Knoten, HashSet<Kante>>();
		public Dictionary<int, Knoten> idMap { get; } = new Dictionary<int, Knoten>();

        public Graph (int AnzahlKnoten, List<Tuple<int, int>> kantenList)
        {
			Dictionary<int, Knoten> knotenMap = new Dictionary<int, Knoten>();
			for (int index = 0; index < AnzahlKnoten; index++)
			{
				Knoten knoten = new Knoten(index);

				if (knotenMap.ContainsKey(index) == false)
				{
					knotenMap.Add(index, knoten);
					graphMap.Add(knoten, new HashSet<Kante>());
				}
			}

			foreach (var tuppleKey in kantenList)
			{
				Knoten knotenFrom = knotenMap[tuppleKey.Item1];
				Knoten knotenTo = knotenMap[tuppleKey.Item2];

				Kante KanteFrom = new Kante(knotenFrom, knotenTo, -1);
                Kante KanteTo = new Kante(knotenTo, knotenFrom, -1);

            if (graphMap.ContainsKey(knotenFrom) && graphMap.ContainsKey(knotenTo)) {
                graphMap[knotenFrom].Add(KanteFrom);
                graphMap[knotenTo].Add(KanteFrom);
            }
			}

			foreach (var item in graphMap.Keys)
			{
				idMap.Add(item.Wert, item);
			}



        }


    }
}
