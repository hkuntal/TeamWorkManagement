using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DisposePattern
{
    class Customer:IDisposable
    {
        private GasFiller objfill = new GasFiller(); 
        public void FillGas()
        {
            Console.WriteLine("Starting to fill the gas");
            //This customer is going to fill gas in the car
            var gasFilled = objfill.FillGas(5);
            if (gasFilled)
            {
                Console.WriteLine("HURRAY the gas was filled in the car successfully");
                Console.WriteLine(objfill.Status); // Null reference exception is thrown here if the object gets dispoased, ie. it is set to null in the Dispose method
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        public void Dispose(bool disposing)
        {
            Thread.Sleep(2);
            if (disposing)
            {
                objfill = null;
            }
            Console.WriteLine("The object was disposed successfully");
        }
    }
}
