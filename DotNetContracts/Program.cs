using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetContracts
{
    /*
     If a contract fails then some kind of popup message is shown but the code contract has to be enabled. 
     */
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine(Calculator.Divide(32, 0));
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("$$ An exception has occurred " + ex.Message);
            }
        }
    }

    public class Calculator
    {
        public static int Divide(int num, int denom)
        {
            Console.WriteLine("Divide called");
            //Contract.Requires(denom != 0, "Denominator cannot be zero");
            Contract.Requires<DivideByZeroException>(denom != 0, "Denominator cannot be zero");
            return num/denom;
        }
    }
}
