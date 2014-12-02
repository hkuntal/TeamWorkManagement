using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisposePattern
{
    class GasFiller:Subject
    {
        public string Status = "Blank";
        // Gas Filler is the subject whose job is to fill the gas
        public bool FillGas(int gallons)
        {
            Console.WriteLine("Fill GAS method called");
            //Fill the required no. of Gallons

            //Make the thread to sleep for the no. of seconds equal to the number of gallons needed to fill the tank
            System.Threading.Thread.Sleep(gallons * 1000);

            this.Status = "Gas Filled";
            //After tthe thread wakes up return true that the gas is filled
            return true;
        }
    }
}
