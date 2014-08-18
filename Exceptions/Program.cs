using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is to check if an exception thrown from a catch block is handled by the outer General exception block
            // Ans: No it is not handled by the outer General exception block. It is thrown back to the caller method catch block.
            TestClass.TestMethodWrapper();
            Console.ReadLine();
        }
    }

    class TestClass
    {

        public static void TestMethodWrapper()
        {
            try
            {

            TestMethod();

            }
            catch (Exception e)
            {
                Console.WriteLine("\n General exception caught in the caller method" + e.GetType());
            }
        }
        private static void TestMethod()
        {
            try
            {
                // Do some operations that throws an exception
                Console.WriteLine("Throwing File not found exception from the TRY block");

                //Throwing file not found exception
                var b = File.ReadAllLines(@"C:\Hariom\KKR.txt");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("\n File not found exception caught");
                Thread.Sleep(1000);
                Console.WriteLine("\n Now throwing Divide by Zero exception");
                Thread.Sleep(1000);
                var a = 5;
                var b = 100/(a - a);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n General exception caught of type" + e.GetType());
                
            }
        }
    }
}
