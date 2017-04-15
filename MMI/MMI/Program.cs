﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class Program
    {
        static void Main(string[] args)
        {
            writeMessage("Hallo Graph!", true);
            int count = countZhk(new CountZhkTief() ,readFile(new ImportKantenList(), @"c:\run\Graph2.txt"));
            writeMessage("Count ZHK: " + count, true);
        }

        static Graph readFile(IParseGraph parseG, string path)
        {
            int counter = 0;
            string line;
            List<string> lines = new List<string>();

            //Kopf lesen
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            line = file.ReadLine();
            writeMessage("Head: " + line);
            lines.Add(line);

            //Rumpf lesen
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);                
                counter++;
            }

            file.Close();
            Console.WriteLine("{0} Zeilen.", counter);

            Graph gra = parseG.parseGraph(lines.ToArray(), false);
            writeMessage("Graph erstellt", true);

            return gra;
        }

        static int countZhk(ICountZusammenhangskomp cZhk, Graph gra)
        {
            return cZhk.CountZhk(gra);
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
