using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class ImportMatrix : IParseGraph
    {
        public Graph parseGraph(string[] lines, bool debug = false)
        {
            List<Kante> kanten = new List<Kante>();
            Dictionary<int, Knoten> knoten = new Dictionary<int, Knoten>();
            string[] lineSplit;

            Knoten kn = null;
            //ueber die Lines / Zeilen
            for (int i = 1; i < lines.Length; i++)
            {
                if (!knoten.TryGetValue(i, out kn))
                {
                    kn = new Knoten(i);
                    knoten.Add(i, kn);
                }
                lineSplit = lines[i].Split('\t');
                Knoten knLink = null;

                //ueber die Elemente einer Zeile
                for (int j = 0; j < lineSplit.Length; j++)
                {
                    int gewicht = Int32.Parse(lineSplit[j]);
                    if(gewicht > 0)
                    {
                        if (!knoten.TryGetValue(j + 1, out knLink))
                        {
                            //Knoten noch nicht in der Liste
                            knLink = new Knoten(j + 1);
                            knoten.Add(j + 1, knLink);
                        }

                        Kante kant = new Kante(kn, knLink, gewicht);
                        kanten.Add(kant);
                        kn.AddKante(kant);
                    }
                }

            }

            if(debug)
            {
                foreach (KeyValuePair<int, Knoten> enty in knoten)
                {
                    Knoten kn1 = enty.Value;
                    Console.WriteLine("Knoten: " + kn1.Wert);
                    List<Kante> kantenZ = kn1.Kanten;
                    foreach (Kante kan in kantenZ)
                    {
                        Console.Write(kan.ToKnoten.Wert + " ## ");
                    }
                    Console.WriteLine(" - ");
                }
            }            

            return new Graph(kanten, knoten);
        }


    }
}
