using System;
using System.Collections.Generic;

namespace MII_SI
{


	class MainClass
	{
		public static void Main(string[] args)
		{
			List<string> verticesList = new List<string>();
			List<Tuple<string, string>> edgesList = new List<Tuple<string, string>>(); 

			string[] lines = System.IO.File.ReadAllLines(@"../../Graph2.txt");

			if (lines.Length < 1)
			{
				return;
			}

			for (int index = 1; index < lines.Length; index++)
			{
				var linesSplited = lines[index].Split('\t');
				if (verticesList.Contains(linesSplited[0]) == false)
				{
					verticesList.Add(linesSplited[0]);
				}

				Tuple<string,string> edge = new Tuple<string, string>( linesSplited[0], linesSplited[1] );
				edgesList.Add(edge);
			}

			//Init graph(verticesList, edgesList)

			Console.WriteLine(string.Format("Anzahl Verticles: {0}", verticesList.Count));
			Console.WriteLine(string.Format("Anzahl Edges: {0}", edgesList.Count));
		}
	}
}
