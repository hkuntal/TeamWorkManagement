using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Log4NetTesting
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Writing to the log file");

                Log.Error("Error - New Session opened");
                Log.Warn("Warn -  Session opened");
                Log.Fatal("Fatal - Session opened");
                // LOg4Net is able to capture all the inner exceptions and log them. There is nothing extra that needs to be done.
                GetException();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Log.Error("An exception has occured", e);
            }
        }
        private static void GetException()
        {
            try
            {
                GetException1();
            }
            catch (Exception e)
            {
                Log.Error("Error occured in GetException", e);
                throw new NullReferenceException("Testing NULL",e);
            }
        
        }
        private static void GetException1()
        {
            try
            {
                GetException2();
            }
            catch (Exception e)
            {
                Log.Error("Error occured in GetException1",e);
                throw;
            }
        }
        private static void GetException2()
        {
            throw new DivideByZeroException();
        }
    }
}
