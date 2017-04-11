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
            Program.readFile(new ImportMatrix());
        }

        static void readFile(IParseGraph parseG)
        {
            int counter = 0;
            string line;
            List<string> lines = new List<string>();

            // Read the file and display it line by line.  
            System.IO.StreamReader file = new System.IO.StreamReader(@"c:\run\Graph3.txt");
            line = file.ReadLine();
            System.Console.WriteLine("Head: " + line);
            while ((line = file.ReadLine()) != null)
            {
                lines.Add(line);                
                counter++;
            }

            file.Close();
            System.Console.WriteLine("There were {0} lines.", counter);

            parseG.parseGraph(lines.ToArray());
            
            // Suspend the screen.  
            System.Console.ReadLine();
        }
    }
}
