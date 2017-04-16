using System;
using System.Collections.Generic;
using System.Linq;
using MII_SI;
using MIISI;
using MMI_SI;

namespace MIISI
{


	class MainClass
	{
		public static void Main(string[] args)
		{
			Dictionary<int, List<int>> kantenMap = new Dictionary<int, List<int>>();
	
			string[] lines = System.IO.File.ReadAllLines(@"../../GraphSI.txt");

			if (lines.Length <= 1)
			{
				return;
			}
			int anzahlKanten = Int32.Parse(lines[0]);

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


			var graph = new MMI_SI.Graph(anzahlKanten, kantenMap);
			var algorithms = new algorithmen();


			List<string> gefundenGraphen = new List<string>();
			var idMap = graph.idMap;

			Knoten startKnoten = idMap.First().Value;
			HashSet<Knoten> visited = new HashSet<Knoten>();

			while (idMap.Count != 0)
			{
                
                var pfad = new List<Knoten>();


				var ergebnis = algorithms.DFS(graph, startKnoten, visited, v => pfad.Add(v));

				string stringPfad = "";

				if (pfad.Count == 0)
				{
					idMap.Remove(startKnoten.Wert);
					if (idMap.Count == 0)
						break;
					startKnoten = idMap.Values.First();
					continue;
				}

				if (idMap.Count > 0)
				{ 
				  startKnoten = idMap.Values.First();
				}

				visited = ergebnis;

				foreach (var knote in pfad)
				{
					stringPfad += string.Format("{0} ", knote.Wert);

				}

				gefundenGraphen.Add(stringPfad);
			}


			Console.WriteLine(string.Format("Anzahl Knoten: {0}", graph.graphMap.Count));
			Console.WriteLine(string.Format("Anzahl Kanten: {0}", (lines.Length - 1)));
			Console.WriteLine(string.Format("Anzahl Graphen: {0}", gefundenGraphen.Count));
			foreach (var item in gefundenGraphen)
			{
				Console.WriteLine(item);
			}
		}
	}
}
