using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class GraphOut
    {
        public static void writeMessage(string Msg, bool needEnter = false)
        {
            Console.WriteLine(Msg);
            if (needEnter)
            {
                Console.Write("Weiter mit <_|");
                Console.ReadLine();
                Console.WriteLine("-ok");
            }
        }

        public static void writeMessage(Kante kant, bool needEnter = false)
        {
            if(kant != null)
            {
                writeMessage(kant.ToString(), needEnter);
            } else
            {
                writeMessage("Kante==null", needEnter);
            }
            
        }

        public static void writeMessage(List<Kante> kanten, bool needEnter = false)
        {
            double gewicht = 0d;
            foreach (Kante kant in kanten)
            {
                writeMessage(kant, needEnter);
                if(kant != null)
                {
                    gewicht += kant.Gewicht;
                }
            }
            writeMessage("--> " + gewicht, needEnter);
        }
    }
}
