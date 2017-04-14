﻿using System;
using System.Collections.Generic;


namespace MMI
{
    public class ImportKantenList : IParseGraph
    {

        public Graph parseGraph(string[] lines, bool debug)
        {
            List<Kante> kanten = new List<Kante>();
            Dictionary<int, Knoten> knoten = new Dictionary<int, Knoten>();
            string[] lineSplit;

            Knoten kn1;
            Knoten kn2;
            Kante kant;

            //ueber die Lines / Zeilen
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                lineSplit = line.Split('\t');
                int knWert1 = Int32.Parse(lineSplit[0]);
                int knWert2 = Int32.Parse(lineSplit[1]);

                if (!knoten.TryGetValue(knWert1, out kn1))
                {
                    kn1 = new Knoten(knWert1);
                    knoten.Add(knWert1, kn1);
                }

                if (!knoten.TryGetValue(knWert2, out kn2))
                {
                    kn2 = new Knoten(knWert2);
                    knoten.Add(knWert2, kn2);
                }

                kant = new Kante(kn1, kn2);
                kanten.Add(kant);
                kn1.AddKante(kant);

            }

            return new Graph(kanten, knoten);
        }
    }
}