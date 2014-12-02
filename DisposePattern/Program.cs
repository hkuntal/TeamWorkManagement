using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DisposePattern
{
    class Program
    {
        private static void Main (string[] args)
        {
            var obj = new TestDisposePattern();
            obj.Test();
        }
    }
    class TestDisposePattern
    {
        // Create the customer object
         Customer objCustomer = new Customer();

        public void Test()
        {
            // Create two threads to - one for the customer to fill the gas and the other to dispose the customer object in between
            var t = new Thread(FillGasForTheCustomer);
            var td = new Thread(DisposeCustomer);
            
            //Start the threads
            t.Start();
            td.Start();

            Console.ReadLine();
        }

        private void FillGasForTheCustomer()
        {
            objCustomer.FillGas();
        }
        private void DisposeCustomer()
        {
            objCustomer.Dispose();
        }
    }
}
