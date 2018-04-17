using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class ImportMatrix : IParseGraph
    {
        public Graph parseGraph(int count, string[] lines, bool debug = false)
        {
            List<Kante> kanten = new List<Kante>();
            List<Knoten> knoten = new List<Knoten>();
            string[] lineSplit;

            Knoten kn = null;
            //ueber die Lines / Zeilen
            for (int i = 1; i < lines.Length; i++)
            {

                if (knoten[i] == null)
                {
                    kn = new Knoten(i);
                    knoten[i] = kn;
                }
                else
                {
                    kn = knoten[i];
                }

                lineSplit = lines[i].Split('\t');
                Knoten knLink = null;

                //ueber die Elemente einer Zeile
                for (int j = 0; j < lineSplit.Length; j++)
                {
                    int gewicht = Int32.Parse(lineSplit[j]);
                    if(gewicht > 0)
                    {
                        if (knoten[j+1] == null)
                        {
                            knLink = new Knoten(j+1);
                            knoten[j+1] = kn;
                        }
                        else
                        {
                            knLink = knoten[i];
                        }

                        Kante kant = new Kante(kn, knLink, gewicht);
                        kanten.Add(kant);
                        kn.AddKante(kant);
                    }
                }

            }

            if(debug)
            {
                foreach (Knoten enty in knoten)
                {
                    Knoten kn1 = enty;
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
