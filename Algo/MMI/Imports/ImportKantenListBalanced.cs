using System;
using System.Globalization;
using System.Collections.Generic;


namespace MMI
{
    public class ImportKantenListBalanced : AbsImportKantenList
    {

        public override Graph parseGraph(int count, string[] lines, bool ungerichtet = true)
        {
            //Parsen nach Culture Symbols
            IFormatProvider formatProf = CultureInfo.CreateSpecificCulture("us-US");
            //--- ende
            List<Kante> kanten = new List<Kante>();
            List<Knoten> knoten = createBalancedKnotenList(count, lines);
            string[] lineSplit;



            Kante kant1;
            Kante kant2;

            //ueber die Lines / Zeilen
            for (int i = count; i < lines.Length; i++)
            {
                Knoten kn1 = null;
                Knoten kn2 = null;

                string line = lines[i];
                lineSplit = line.Split('\t');
                int knWert1 = Int32.Parse(lineSplit[0]);
                int knWert2 = Int32.Parse(lineSplit[1]);
                double kantKosten = Double.Parse(lineSplit[2], formatProf);
                double kantKapa = Double.Parse(lineSplit[3], formatProf);

                if (knoten[knWert1] != null && knoten[knWert1] != null)
                {
                    kn1 = knoten[knWert1];
                    kn2 = knoten[knWert2];

                    kant1 = new Kante(kn1, kn2, kantKosten, kantKapa);
                    kanten.Add(kant1);
                    kn1.AddKante(kant1);

                    if (ungerichtet)
                    {
                        kant2 = new Kante(kn2, kn1, kantKosten, kantKapa);
                        kanten.Add(kant2);
                        kn2.AddKante(kant2);
                    }
                }
            }

            kanten.Sort();
            return new Graph(kanten, knoten);
        }

        public List<Knoten> createBalancedKnotenList(int count, string[] lines)
        {
            List<Knoten> knoten = new List<Knoten>();

            double bala = 0d;
            for (int i = 0; i < count; i++)
            {
                bala = Double.Parse(lines[i + 1]);
                knoten.Add(new Knoten(i, bala));
            }

            return knoten;
        }
    }
}