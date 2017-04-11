using System;
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
            Console.WriteLine("Hallo Mama!");
            Console.ReadKey();
            Program.readFile();
        }

        static void readFile()
        {
            int counter = 0;
            string line;
            string[] lineSplit;

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\run\Graph3.txt");
            line = file.ReadLine();
            System.Console.WriteLine("Head: " + line);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                System.Console.WriteLine("");
                lineSplit = line.Split('\t');
                foreach(string str in lineSplit)
                {
                    System.Console.Write(str + " ## ");
                }
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);
            // Suspend the screen.  
            System.Console.ReadLine();
        }
    }
}
