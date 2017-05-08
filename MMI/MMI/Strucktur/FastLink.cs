using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMI
{
    class FastLink
    {
        public Kante K;
        public FastLink Front;
        public FastLink Back;

        public FastLink(Kante kan)
        {
            K = kan;
        }

        public FastLink(Kante kan, FastLink back)
        {
            K = kan;
            Back = back;
            Back.Front = this;
        }

        public FastLink(Kante kan, FastLink front, FastLink back)
        {
            K = kan;
            Front = front;
            Back = back;

            Front.Back = this;
            Back.Front = this;
        }

        public void removeMe()
        {
            Front.Back = Back;
            Back.Front = Front;
        }
    }
}
