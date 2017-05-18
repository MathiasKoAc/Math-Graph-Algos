﻿using System;
using System.Collections.Generic;
using MMI.Algos;

namespace MMI
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gerichtet = false;
            writeMessage("Hallo Graph!", true);

            //Graph g = readFile(new ImportKantenListGew(), @"files/MK_10_5.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_1_2.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_1_20.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_10_20.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_10_200.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_100_200.txt", true);

            Graph g = readFile(new ImportKantenListGew(), @"files/K_10e.txt", gerichtet);

            //Kruskal
            //double count = g.countMST(new Kruskal());

            //Prim
            //double count = new Prim().CountMST(g, g.Konten[0]);

            //PrimFaster
            //double count = new PrimFast().CountMST(g, g.Konten[0]);

            //NearestN
            //double count = g.countTSPTripp(new NearestNeigbor(), true);

            //DoubleTree
            //double count = g.countTSPTripp(new DoubleTree(), false);

            //writeMessage("Count TSP: " + count, true);


            //AlleTouren
            BackTrackAll bTA = new BackTrackAll();
            var touren = new List<List<Kante>>();
            bTA.allRoundTripps(g, g.Knoten[0], out touren);

            foreach(List<Kante> kanten in touren)
            {
                foreach(Kante kant in kanten)
                {
                    writeMessage(kant.ToString(), false);
                }
                writeMessage("------", false);
            }

        }

        static Graph readFile(IParseGraph parseG, string path, bool gerichtet)
        {
            int anzKnoten = 0; //aus Head
            int counter = 0;
            string line;
            List<string> lines = new List<string>();

            //Kopf lesen
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            line = file.ReadLine();
            writeMessage("Head: " + line);
            lines.Add(line);
            anzKnoten = Int32.Parse(line);

            //Rumpf lesen
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);                
                counter++;
            }

            file.Close();
            Console.WriteLine("{0} Zeilen.", counter);

            Graph gra = parseG.parseGraph(anzKnoten, lines.ToArray(), !gerichtet);
            writeMessage("Graph erstellt");

            writeMessage("Anz Knoten: " + gra.getAnzKnoten());
            writeMessage("Anz Kanten: " + gra.getAnzKanten(), true);


            return gra;
        }

        static void writeMessage (string Msg, bool needEnter = false)
        {
            Console.WriteLine(Msg);
            if(needEnter)
            {
                Console.Write("Weiter mit <_|");
                Console.ReadLine();
                Console.WriteLine("-ok");
            }
        }
    }
}
