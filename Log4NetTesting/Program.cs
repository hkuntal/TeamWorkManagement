using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Log4NetTesting
{
    class Program
    {
        //private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
        private static readonly ILog Log1 = LogManager.GetLogger("AuditLoggerOne");
        private static readonly ILog Log = LogManager.GetLogger("Program");
        

        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Writing to the log file");

                string s = null;
                Console.WriteLine(Convert.ToString(s));
                ThreadContext.Properties["Hariom"] = "kuntal";

                //Log.Error("Error - New Session opened"); // shows as error synbol in event viewer
                //Log.Warn("Warn -  Session opened"); // shows as warn symbol in event viewer
                //Log.Fatal("Fatal - Session opened"); // shows as error symbol in event viewer
                //Log.ErrorFormat("Erro occured {0}", 1266); // shows as Error symbol in event viewer
                Log.Info("Testing info logging"); // Gets logged as information icon
                Log1.Info("Testing info logging");
                //Log.Debug("Testing Debug Info"); // Gets logged as information icon
                
                // LOg4Net is able to capture all the inner exceptions and log them. There is nothing extra that needs to be done.
                // Enable the below line to see the exception logs
                GetException();

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Log.Error("An exception has occured", e);
                Log.ErrorFormat(CultureInfo.CurrentCulture, "Harioms Kuntal exception has occured {0}", e);
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
