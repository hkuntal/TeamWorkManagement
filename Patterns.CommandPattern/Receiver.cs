using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    class Light
    {
        public void On()
        {
            Console.WriteLine("Light switched On");
        }

        public void Off()
        {
            Console.WriteLine("Light switched Off");
        }
    }
    class Stereo
    {
        public void On()
        {
            Console.WriteLine("Stereo switched On");
        }

        public void Off()
        {
            Console.WriteLine("Stereo switched Off");
        }
    }
}
