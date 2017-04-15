using System;
using System.Collections.Generic;
using MII_SI;
using MIISI;
using MMI_SI;

namespace MIISI
{


	class MainClass
	{
		public static void Main(string[] args)
		{
			List<int> verticesList = new List<int>();
			List<Tuple<int, int>> edgesList = new List<Tuple<int, int>>();

	


			string[] lines = System.IO.File.ReadAllLines(@"../../Graph2.txt");

			if (lines.Length <= 1)
			{
				return;
			}
			int anzahlKanten = Int32.Parse(lines[0]);

			for (int index = 1; index < lines.Length; index++)
			{
				var linesSplited = lines[index].Split('\t');
				int idKey = Int32.Parse(linesSplited[0]);

				Tuple<int,int> edge = new Tuple<int, int>( Int32.Parse(linesSplited[0]), Int32.Parse(linesSplited[1]) );
				edgesList.Add(edge);
			}


			var graph = new MMI_SI.Graph(anzahlKanten, edgesList);
			var algorithms = new algorithmen();

			var pfad = new List<Knoten>();

			var ergebnis = algorithms.DFS(graph, graph.idMap[2],  v => pfad.Add(v));

			foreach (var item in pfad)
			{

			}


			Console.WriteLine(string.Format("Anzahl Verticles: {0}", anzahlKanten));
			Console.WriteLine(string.Format("Anzahl Edges: {0}", edgesList.Count));
		}
	}
}
