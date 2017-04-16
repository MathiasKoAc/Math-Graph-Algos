using System;
using System.Collections;
using System.Collections.Generic;
using MMI_SI;

namespace MII_SI
{
	public class algorithmen
	{
        public algorithmen()
		{
		}

    	public HashSet<Knoten> DFS(MMI_SI.Graph graph, Knoten start, HashSet<Knoten> visited, Action<Knoten> preVisit = null)
		{
			if (!graph.graphMap.ContainsKey(start))
			{
				return visited;
			}        

			var queue = new Queue<Knoten>();
			queue.Enqueue(start);

			while (queue.Count > 0) {
			var knoten = queue.Dequeue();

			if (visited.Contains(knoten))
				 continue;

			if (preVisit != null)

			preVisit(knoten);

			visited.Add(knoten);

				var kanten = graph.graphMap[knoten];

			foreach(var kante in kanten)
				if (!visited.Contains(kante.ToKnoten))
					 queue.Enqueue(kante.ToKnoten);
			}

			    return visited;
	}
	}
}
