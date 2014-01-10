using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesThreading
{
    public delegate int Calculate(int a , int b ); 
    class DelegateTest
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        public void Calculator(Calculate obj)
        {
            int a = obj(5, 6);
            Console.WriteLine(a.ToString());
        }
    }
}
