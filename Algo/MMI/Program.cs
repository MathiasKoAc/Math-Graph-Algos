using System;
using System.Collections.Generic;
using MMI.Algos;

namespace MMI
{
    class Program
    {
        const bool gerichtet = true;
        const bool ungerichtet = false;
        const bool needEnter = true;
        const bool doNotNeedEnter = false;

        static void Main(string[] args)
        {
            GraphOut.writeMessage("Hallo Graph!", true);

            //--- ZHK ---//
            //Graph g = Reader.readFile(new ImportKantenList(), @"files/Graph4.txt", gerichtet);
            //GraphOut.writeMessage("ZHK Tief: " + new CountZhkTief().CountZhk(g), true);
            //GraphOut.writeMessage("ZHK Breit: " + new CountZhkBreit().CountZhk(g), true);

            //--- MST ---//
            List<Kante> kruskalKanten;
            Graph g = Reader.readFile(new ImportKantenListGew(), @"files/G_100_200.txt", ungerichtet);
            GraphOut.writeMessage("MST Prim2: " + new Prim2().CountMST(g, out kruskalKanten, g.Knoten[0]), needEnter);
            GraphOut.writeMessage("MST Kruskal: " + new Kruskal().CountMST(g, out kruskalKanten), needEnter);
            //GraphOut.writeMessage("MST Prim: " + new Prim().CountMST(g, out kruskalKanten, g.Knoten[0]), needEnter);


            //--- TSP ---//
            //List<Knoten> knotenList;
            //Graph g = Reader.readFile(new ImportKantenListGew(), @"files/K_10.txt", ungerichtet);
            //GraphOut.writeMessage("TSP DT-Kruskal: " + new DoubleTree().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
            //GraphOut.writeMessage("TSP DT2-Kruskal: " + new DoubleTree2().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
            //GraphOut.writeMessage("TSP NearestNeigbor: " + new NearestNeigbor().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
        }
    }
}
