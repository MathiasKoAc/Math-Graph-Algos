using System;
using System.Collections.Generic;


namespace MMI
{
    public class ImportKantenList : AbsImportKantenList
    {

        public override Graph parseGraph(int count, string[] lines, bool ungerichtet = true)
        {
            List<Kante> kanten = new List<Kante>();
            List<Knoten> knoten = createKnotenDict(count);
            string[] lineSplit;

            Knoten kn1;
            Knoten kn2;
            Kante kant1;
            Kante kant2;

            //ueber die Lines / Zeilen
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                lineSplit = line.Split('\t');
                int knWert1 = Int32.Parse(lineSplit[0]);
                int knWert2 = Int32.Parse(lineSplit[1]);

                if (knoten[knWert1] == null)
                {
                    kn1 = new Knoten(knWert1);
                    knoten[knWert1] = kn1;
                } else
                {
                    kn1 = knoten[knWert1];
                }

                if (knoten[knWert2] == null)
                {
                    kn2 = new Knoten(knWert2);
                    knoten[knWert2] = kn2;
                } else
                {
                    kn2 = knoten[knWert2];
                }

                kant1 = new Kante(kn1, kn2);
                kanten.Add(kant1);
                kn1.AddKante(kant1);

                if (ungerichtet)
                {
                    kant2 = new Kante(kn2, kn1);
                    kanten.Add(kant2);
                    kn2.AddKante(kant2);
                }
            }

            return new Graph(kanten, knoten);
        }
    }
}