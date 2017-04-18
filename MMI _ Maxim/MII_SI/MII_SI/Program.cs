using System;
using System.Collections.Generic;
using System.Linq;
using MII_SI;
using MIISI;
using MMI_SI;

namespace MIISI
{
	enum SucheMetode
	{ 
		DFSRekrusiv,
		DFS,
		BFS
	}

	class MainClass
	{


		public static void Main(string[] args)
		{

			Dictionary<int, List<int>> initData = readFileAndInitMap();

			var graph = new MMI_SI.Graph(initData);
			var algorithms = new algorithmen();

			SucheMetode suche = SucheMetode.DFS;

			List<string> gefundenGraphen = new List<string>();
			var idMap = graph.idMap;

			Knoten startKnoten = idMap.First().Value;
			HashSet<Knoten> visited = new HashSet<Knoten>();

			while (idMap.Count != 0)
			{
				string stringPfad = "";
				var pfad = new List<Knoten>();

				switch (suche)	
				{
					case(SucheMetode.BFS):
						algorithms.BFS(graph, startKnoten, visited, v => pfad.Add(v));
						break;
					case(SucheMetode.DFSRekrusiv):
						algorithms.DFSRekrusiv(graph, startKnoten, visited, v => pfad.Add(v));
						break;
					case(SucheMetode.DFS):
						algorithms.DFS(graph, startKnoten, visited, v => pfad.Add(v));
						break;
					default:
						break;
				}



				if (pfad.Count == 0)
				{
					idMap.Remove(startKnoten.Wert);
					if (idMap.Count == 0)
						break;
					startKnoten = idMap.Values.First();
					continue;
				}

				foreach (var knote in pfad)
				{
					//stringPfad += " " + knote.Wert.ToString();

				}

				gefundenGraphen.Add(stringPfad);

				if (graph.graphMap.Keys.Count == visited.Count)
				{
					break;
				}

				if (idMap.Count > 0)
				{
					bool found = false;

					while (!found)
					{
						startKnoten = idMap.Values.First();

						if (visited.Contains(startKnoten))
						{
							idMap.Remove(startKnoten.Wert);
							if (idMap.Count == 0)
								break;
						}
						else
						{
							found = true;
						}
					}
				}
			}

			Console.WriteLine(string.Format("Anzahl Knoten: {0}", graph.graphMap.Count));
			Console.WriteLine(string.Format("Anzahl Kanten: {0}", graph.anzahlKanten));
			Console.WriteLine(string.Format("Anzahl Graphen: {0}", gefundenGraphen.Count));
			foreach (var item in gefundenGraphen)
			{
				Console.WriteLine(item);
			}
		}

		public static Dictionary<int, List<int>> readFileAndInitMap()
		{
			Dictionary<int, List<int>> kantenMap = new Dictionary<int, List<int>>();

			string[] lines = System.IO.File.ReadAllLines(@"../../G_100_200.txt");

			if (lines.Length <= 1)
			{
				return new Dictionary<int, List<int>>();
			}

			for (int index = 1; index < lines.Length; index++)
			{
				var linesSplited = lines[index].Split('\t');

				int fromKnoten = Int32.Parse(linesSplited[0]);
				int toKnoten = Int32.Parse(linesSplited[1]);

				if (kantenMap.ContainsKey(fromKnoten))
				{
					kantenMap[fromKnoten].Add(toKnoten);
				}
				else
				{
					List<int> listTo = new List<int>();
					listTo.Add(toKnoten);
					kantenMap.Add(fromKnoten, listTo);
				}
			}
			return kantenMap;
		}
	}
}
