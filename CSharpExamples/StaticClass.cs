using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiServicesClient
{
    public class StaticClass
    {
        public static bool StaticMember1 = PrintSomething("StaticMember1");
        public static bool StaticMember2 = PrintSomething("StaticMember2");
        public static bool StaticMember3 = PrintSomething("StaticMember3");
        public static Lazy<bool> StaticMember4 = new Lazy<bool>(() => PrintSomething("Lazy member loaded StaticMember4"));
        private static bool PrintSomething(string s)
        {
            Console.WriteLine(s);
            return true;
        }

        static StaticClass()
        {
            Console.WriteLine("Static constructor called");
        }
    }
}
