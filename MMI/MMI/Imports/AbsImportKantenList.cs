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

        /// <summary>
        /// Erstellt aus den Lines einer Importdatei einen Graph
        /// </summary>
        /// <param name="count"> Anzahl der Knoten</param>
        /// <param name="lines"> Lines der Datei</param>
        /// <param name="ungerichtet"> gibt an ob es ein ungerichteter Graph ist, true für ungerichtet</param>
        /// <returns></returns>
        public abstract Graph parseGraph(int count, string[] lines, bool ungerichtet = true);
        
    }
}