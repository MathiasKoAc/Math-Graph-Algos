﻿using System;
using System.Collections.Generic;

namespace MMI
{
    class Program
    {
        static void Main(string[] args)
        {
            writeMessage("Hallo Graph!", true);
            int count = readFile(new ImportKanLisUngrGew(), @"files/G_1_20.txt").countZhk(new CountZhkBreit());
            writeMessage("Count ZHK: " + count, true);
        }

        static Graph readFile(IParseGraph parseG, string path)
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

            Graph gra = parseG.parseGraph(anzKnoten, lines.ToArray(), false);
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
                Console.WriteLine("Weiter mit <_|");
                Console.ReadLine();
            }
        }
    }
}
