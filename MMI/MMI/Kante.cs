﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    public class Kante
    {
        private double _gewicht;
        private Knoten toKnoten;
        private Knoten fromKnoten;

        public Kante(Knoten fromK, Knoten toK, double gewicht)
        {
            fromKnoten = fromK;
            toKnoten = toK;
            _gewicht = gewicht;
        }

        public Kante(Knoten fromK, Knoten toK)
        {
            fromKnoten = fromK;
            toKnoten = toK;
        }

        public Knoten ToKnoten
        {
            get
            {
                return toKnoten;
            }
        }

        public Knoten FromKnoten
        {
            get
            {
                return fromKnoten;
            }
        }
    }
}
