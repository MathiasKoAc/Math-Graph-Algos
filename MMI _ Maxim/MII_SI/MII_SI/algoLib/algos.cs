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

		public void BFS(MMI_SI.Graph graph, Knoten start, HashSet<Knoten> visited, Action<Knoten> preVisit = null)
		{
			//Wenn start Knoten nicht im Map ist, dann ist hier schluss.
			if (!graph.graphMap.ContainsKey(start))
			{
				return;
			}

			var queue = new Queue<Knoten>();
			queue.Enqueue(start);

			//Mache solange bis Warteschlange größer als 0 ist
			while (queue.Count > 0)
			{
				var knoten = queue.Dequeue();

				if (visited.Contains(knoten))
					continue;
				//pfad bauen
				if (preVisit != null)
					preVisit(knoten);

				visited.Add(knoten);

				var kanten = graph.graphMap[knoten];

				foreach (var kante in kanten)
					if (!visited.Contains(kante.ToKnoten))
						queue.Enqueue(kante.ToKnoten);
			}


		}

		public void DFS(Graph graph, Knoten start, HashSet<Knoten> visited, Action<Knoten> preVisit = null)
		{
			if (!graph.graphMap.ContainsKey(start))
				return;

			var stack = new Stack<Knoten>();
			stack.Push(start);

			//pfad bauen
			if (preVisit != null)
                preVisit(start);

			while (stack.Count > 0)
			{
				var knoten = stack.Pop();

				if (visited.Contains(knoten))
					continue;

				visited.Add(knoten);

               var kanten = graph.graphMap[knoten];

				foreach (var kante in kanten)
					if (!visited.Contains(kante.ToKnoten))
						stack.Push(kante.ToKnoten);
			}


		}

		public void DFSRekrusiv(Graph graph, Knoten start, HashSet<Knoten> visited, Action<Knoten> preVisit = null)
		{
			Traverse(graph, start, visited, preVisit);
		}

		private void Traverse(Graph graph, Knoten knoten, HashSet<Knoten> visited, Action<Knoten> preVisit = null)
		{
			//Markiere als Besucht 
			visited.Add(knoten);

			//pfad bauen
			if (preVisit != null)
				preVisit(knoten);

			//Mach nur dann weiter, wenn Knoten im Map ist
			if (graph.graphMap.ContainsKey(knoten))
			{

				var kanten = graph.graphMap[knoten];

				foreach (var kante in kanten)
					if (!visited.Contains(kante.ToKnoten))
						Traverse(graph, kante.ToKnoten, visited, preVisit);
			}
		}
	}
}
