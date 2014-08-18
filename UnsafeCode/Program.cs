using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnsafeCode
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //    }
    //}

    class Test
    {
        public int x;
    }
    //http://www.c-sharpcorner.com/UploadFile/gregory_popek/WritingUnsafeCode11102005040251AM/WritingUnsafeCode.aspx
    class SimpleTest
    {
        [STAThread]
        static void Main(string[] args)
        {
            unsafe
            {
                int* i = GetValue();    
                //fixed (int* i = GetValue())
                //{
                    //Force the garbage collection
                    System.GC.Collect();
                    Console.WriteLine("after g.c.: " + *i);
                //}
            }
            // From this example it seems that even if unsafe pointers are pointing to a address the garbage collection still cleans and consolidates the memory
            // Prints value 100 and 12
        }

        private static unsafe int* GetValue()
        {
            Test test = new Test();
            int* pi;
            fixed (int* px = &test.x)
            {
                *px = 100;
                pi = px;
            }
            Console.WriteLine("before g.c.: " + *pi);
            return pi;
            //Force the garbage collection
//            System.GC.Collect(2);
            //System.GC.Collect();
            //Console.WriteLine("after g.c.: " + *pi);
        }
    }

}
