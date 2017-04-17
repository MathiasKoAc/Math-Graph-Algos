using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI_SI
{
    public class Graph
    {

		public Dictionary<Knoten, HashSet<Kante>> graphMap  { set;get; }  = new Dictionary<Knoten, HashSet<Kante>>();
		public Dictionary<int, Knoten> idMap { set;get; } = new Dictionary<int, Knoten>();

		public Graph (int AnzahlKnoten, Dictionary<int, List<int>> kantenMap)
        {
			Dictionary<int, Knoten> knotenMap = new Dictionary<int, Knoten>();
			foreach (var values in kantenMap)
			{

				Knoten knoten = new Knoten(values.Key);


				if (knotenMap.ContainsKey(knoten.Wert) == false)
				{
					knotenMap.Add(knoten.Wert, knoten);
					graphMap.Add(knoten, new HashSet<Kante>());
				}

				foreach (var knotenTo in values.Value)
				{
					knoten = new Knoten(knotenTo);
					if (knotenMap.ContainsKey(knoten.Wert) == false)
					{
						knotenMap.Add(knoten.Wert, knoten);
						graphMap.Add(knoten, new HashSet<Kante>());
					}
				}
			}

			foreach (var valueMap in kantenMap)
			{
				Knoten knotenFrom = knotenMap[valueMap.Key];

				foreach (var knotenToIndex in valueMap.Value)
				{

					Knoten knotenTo = knotenMap[knotenToIndex];

					Kante KanteFrom = new Kante(knotenFrom, knotenTo, -1);
					Kante KanteTo = new Kante(knotenTo, knotenFrom, -1);

					if (graphMap.ContainsKey(knotenFrom) && graphMap.ContainsKey(knotenTo))
					{
						graphMap[knotenFrom].Add(KanteFrom);
						graphMap[knotenTo].Add(KanteTo);
					}

				}
			}

			foreach (var item in graphMap.Keys)
			{
				idMap.Add(item.Wert, item);
			}
        }




    }
}
