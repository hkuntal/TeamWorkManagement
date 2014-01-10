using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderstandingDelegates
{
    public  delegate string TestDelegate(string str);
    class Program
    {
        static void Main(string[] args)
        {
            //Creating an instance of the delegate and calling it
            TestDelegate td = new TestDelegate(Program.Method);
            Console.WriteLine(td("Kuntal"));
            //Following are the different ways of calling and passing a delegate
            TestDelegate((str) => str + "kkk");
            TestDelegate(Program.Method);
            TestDelegate(td);
            Console.ReadKey();
        }

        public static string Method(string str)
        {
            return "Hariom" + str;
        }

        public static void TestDelegate(TestDelegate obj)
        {
              Console.WriteLine(obj("HHH"));          
        }
    }
}
