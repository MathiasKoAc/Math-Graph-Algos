using System;
using System.Collections.Generic;


namespace MMI
{
    public abstract class AbsImportKantenList : IParseGraph
    {

        protected Dictionary<int, Knoten> createKnotenDict(int count)
        {
            Dictionary<int, Knoten> knoten = new Dictionary<int, Knoten>();

            for(int i = 0; i < count; i++)
            {
                knoten.Add(i, new Knoten(i));
            }

            return knoten;
        }

        public abstract Graph parseGraph(int count, string[] lines, bool ungerichtet = true);
        
    }
}