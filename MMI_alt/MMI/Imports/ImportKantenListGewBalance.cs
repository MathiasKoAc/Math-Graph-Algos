using System;
using System.Globalization;
using System.Collections.Generic;


namespace MMI
{
    public class ImportKantenListGewBalance : AbsImportKantenList
    {

        public override Graph parseGraph(int count, string[] lines, bool ungerichtet = true)
        {
            if(count > lines.Length)
            {
                Console.WriteLine("Error Eingabedate!");
                return null;
            }

            //Parsen nach Culture Symbols
            IFormatProvider formatProf = CultureInfo.CreateSpecificCulture("us-US");
            //--- ende
            List<Kante> kanten = new List<Kante>();
            Dictionary<int, Knoten> knoten = createKnotenDict(count); //TODO privat FUNKKTIONNN UNTEN
            string[] lineSplit;

            Knoten kn1;
            Knoten kn2;
            Kante kant1;
            Kante kant2;

            //ueber die Lines / Zeilen
            for (int i = count+1; i < lines.Length; i++)
            {
                string line = lines[i];
                lineSplit = line.Split('\t');
                int knWert1 = Int32.Parse(lineSplit[0]);
                int knWert2 = Int32.Parse(lineSplit[1]);
                double kantKapazitaet = Double.Parse(lineSplit[3], formatProf);
                double kantKosten = Double.Parse(lineSplit[2], formatProf);

                if (!knoten.TryGetValue(knWert1, out kn1))
                {
                    kn1 = new Knoten(knWert1);
                    knoten.Add(knWert1, kn1);
                }

                if (!knoten.TryGetValue(knWert2, out kn2))
                {
                    kn2 = new Knoten(knWert2);
                    knoten.Add(knWert2, kn2);
                }

                kant1 = new Kante(kn1, kn2, kantKapazitaet);
                kant1.Kosten = kantKosten;
                kanten.Add(kant1);
                kn1.AddKante(kant1);

                if (ungerichtet)
                {
                    kant2 = new Kante(kn2, kn1, kantKapazitaet);
                    kant2.Kosten = kantKosten;
                    kanten.Add(kant2);
                    kn2.AddKante(kant2);
                }               

            }

            kanten.Sort();

            return new Graph(kanten, knoten);
        }

        private void parseBalance(int count, string[] lines, out Dictionary<int, Knoten> knotens)
        {
            knotens = createKnotenDict(count);

            for (int i = 1; i < count; i++) {
                knotens[i].Balance = Int32.Parse(lines[i]);
            }
        }
    }
}