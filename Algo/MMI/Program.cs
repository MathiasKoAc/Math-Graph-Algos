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
            Graph g;

            //--- ZHK ---//
            //Graph g = Reader.readFile(new ImportKantenList(), @"files/Graph4.txt", gerichtet);
            //GraphOut.writeMessage("ZHK Tief: " + new CountZhkTief().CountZhk(g), true);
            //GraphOut.writeMessage("ZHK Breit: " + new CountZhkBreit().CountZhk(g), true);

            //--- MST ---//
            /*List<Kante> kruskalKanten;
            Graph g = Reader.readFile(new ImportKantenListGew(), @"files/G_100_200.txt", ungerichtet);
            GraphOut.writeMessage("MST Prim2: " + new Prim2().CountMST(g, out kruskalKanten, g.Knoten[0]), needEnter);
            GraphOut.writeMessage("MST Kruskal: " + new Kruskal().CountMST(g, out kruskalKanten), needEnter);
            GraphOut.writeMessage("MST Prim: " + new Prim().CountMST(g, out kruskalKanten, g.Knoten[0]), needEnter);
            */

            //--- TSP ---//
            //List<Knoten> knotenList;
            //Graph g = Reader.readFile(new ImportKantenListGew(), @"files/K12.txt", gerichtet);
            //GraphOut.writeMessage("TSP DT-Kruskal: " + new DoubleTree().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
            //GraphOut.writeMessage("TSP DT2-Kruskal: " + new DoubleTree2().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
            //GraphOut.writeMessage("--NearestNeigbor--", needEnter);
            //GraphOut.writeMessage("TSP NearestNeigbor: " + new NearestNeigbor().roundTripp(g, g.Knoten[0], out knotenList), needEnter);
            //GraphOut.writeMessage("TSP BackTrackAll2: " + new BackTrackAll2().roundTripp(g, g.Knoten[0], out knotenList), needEnter);

            //--- SPP ---//
            /*List<Knoten> knotenList;
            Graph g = Reader.readFile(new ImportKantenListKosten(), @"files/Wege3.txt", ungerichtet);
            //GraphOut.writeMessage("SPP Dijkstra: " + new Dijkstra().ShortestWay(g, g.Knoten[0], g.Knoten[1], out knotenList), needEnter);
            //GraphOut.writeMessage(knotenList);

            try
            {
                GraphOut.writeMessage("SPP MoorBellmanFord: " + new MoorBellmanFord().ShortestWay(g, g.Knoten[2], g.Knoten[0], out knotenList), needEnter);
                GraphOut.writeMessage(knotenList);
            } catch (NegativCycleExeption)
            {
                GraphOut.writeMessage("SPP MoorBellmanFord: Nagativer Cycle gefunden", needEnter);
            }
            */
            //--- MFP ---//
            /*
            g = Reader.readFile(new ImportKantenListGew(), @"files/fluss.txt", gerichtet);
            GraphOut.writeMessage("MFP EdmondsKarp: " + new EdmondsKarp().calcMFP(g, g.Knoten[0],g.Knoten[7]), needEnter);
            */

            //--- KMF ---//
            g = Reader.readFile(new ImportKantenListBalanced(), @"files/Kostenminimal4.txt", gerichtet);
            
            try
            {
                GraphOut.writeMessage("KMF SSP: " + new SuccessiveShortestPath().calcKMF(g));
                GraphOut.writeMessage("KMF CCA: " + new CycleCancelling().calcKMF(g));
            }
            catch (NotBflussException e)
            {
                GraphOut.writeMessage(e.Message);
            }

            GraphOut.writeMessage("--##--\nEnde ", needEnter);
        }
    }
}
