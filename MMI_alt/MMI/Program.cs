using System;
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

            Graph g = readFile(new ImportKantenListGew(), @"files/G_1_2.txt", gerichtet);

            writeMessage("BackTrack all: " + new CountZhkTief().CountZhk(g), true);
        }

        static void doKostenMinFluss()
        {
            bool gerichtet = true;

            Graph g = readFile(new ImportKantenListGewBalance(), @"files/Kostenminimal1.txt", gerichtet);
        }

        static void doMaxFluss()
        {
            bool gerichtet = true;
            bool debug = false;

            //Graph g = readFile(new ImportKantenListGew(), @"files/Fluss.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/G_1_2.txt", gerichtet);
            Graph g = readFile(new ImportKantenListGew(), @"files/G_1_200.txt", gerichtet);
            double maxFlussWert =  new EdmundsKarp().calcMaxFluss(g, g.Knoten[0], g.Knoten[7], debug);
            writeMessage("Max-Fluss-Wert: " + maxFlussWert, true);
        }

        static void doDijkstra()
        {
            bool gerichtet = true;

            Graph g = readFile(new ImportKantenListGew(), @"files/Wege1.txt", gerichtet);
            Dijkstra Dij = new Dijkstra();
            Dij.sortestWay(ref g, g.Knoten[2]);
            writeMessage("Kürzester Weg: " + g.Knoten[0].Distance, true);
        }

        static void doBellManFord()
        {
            bool gerichtet = true;

            //Graph g = readFile(new ImportKantenListGew(), @"files/Wege1.txt", gerichtet);
            //Graph g = readFile(new ImportKantenListGew(), @"files/Wege2.txt", gerichtet);
            Graph g = readFile(new ImportKantenListGew(), @"files/Wege3.txt", gerichtet);

            BellmanFord BellFord = new BellmanFord();
            BellFord.sortestWay(ref g, g.Knoten[2], out Knoten CheckNegNoten);
            writeMessage("Kürzester Weg: " + g.Knoten[0].Distance, true);
        }

        static void doAlleTouren()
        {
            bool gerichtet = true;

            Graph g = readFile(new ImportKantenListGew(), @"files/K_10.txt", gerichtet);

            BackTrackAll bTA = new BackTrackAll();
            var touren = new List<List<Kante>>();
            var besttour = new List<Kante>();
            double gewicht = bTA.allRoundTripps(g, g.Knoten[0], out touren, out besttour, false);
            writeMessage("berechnet", true);

            writeMessage("Beste Tour: ", false);
            writeMessage(besttour, false);
            writeMessage("", false);

            writeMessage("Einige andere Touren (" + touren.Count + ")", true);

            for (int i = 0; i < 10 && i < touren.Count; i++)
            {
                List<Kante> kanten = touren[i];
                writeMessage(kanten);
                writeMessage("------", false);
            }
            writeMessage("-", true);
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

        static void writeMessage(Kante kant, bool needEnter = false)
        {
            writeMessage(kant.ToString(), needEnter);
        }

        static void writeMessage(List<Kante> kanten, bool needEnter = false)
        {
            double gewicht = 0d;
            foreach(Kante kant in kanten)
            {
                writeMessage(kant, needEnter);
                gewicht += kant.Gewicht;
            }
            writeMessage("--> " + gewicht, needEnter);
        }
    }
}
