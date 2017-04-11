using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class ImportMatrix : IParseGraph
    {
        public Graph parseGraph(string[] lines)
        {
            foreach(string line in lines)
            {
                //lineSplit = line.Split('\t');
                //foreach (string str in lineSplit)
                //{
                //    System.Console.Write(str + " ## ");
                //}                
            }

            //TODO
            return new Graph();            
        }
    }
}
