using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleRequestResponse
{
    public class ProcessRequest
    {
        public static void ExecuteRequest(Action method)
        {
            try
            {
                method();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error has occured - " + ex.Message + "\n" + "Press any key to exit.");
                Console.ReadKey();
            }
        }

    }
}
