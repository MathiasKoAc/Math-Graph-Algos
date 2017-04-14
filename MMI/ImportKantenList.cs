using System;


namespace MMI
{
    public class ImportKantenList : IParseGraph
    {
        public ImportKantenList()
        {
            List<Kante> kanten = new List<Kante>();
            Dictionary<int, Knoten> knoten = new Dictionary<int, Knoten>();
            string[] lineSplit;

            Knoten kn = null;
            //ueber die Lines / Zeilen
            for (int i = 1; i < lines.Length; i++)
            {
                //ueber die Elemente einer Zeile
                for (int j = 0; j < lineSplit.Length; j++)
                {

                }
        }
    }
}