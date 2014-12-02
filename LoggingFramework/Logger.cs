using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LoggingFramework
{
    public static class Logger
    {

        private static readonly StreamWriter LoggerObject = GetLoggerObject();

        public static void LogInformation(string message)
        {
            //LogMessage(message);
        }
        public static void LogException(string message)
        {
            //LogMessage(message);
        }
        private static StreamWriter GetLoggerObject()
        {
            //create the stream writer object
            string fileName = "Log_" + DateTime.Now.ToString("yyyy-MM-dd hh:mm").Replace(":","-") + ".txt";
            string path = @"C:\hariom\Logs\" + fileName;
            StreamWriter sw = null;
            sw = new StreamWriter(path, true);
            return sw;
        }
        private static void LogMessage(string message)
        {
            try
            {

            //System.Diagnostics.Debug.WriteLine(message);
            ////TODO: Try to use Async here
            //LoggerObject.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fffffff") + ": " + message);
            //LoggerObject.Flush();

            }
            catch (System.IO.IOException)
            {
                //Some other process is using the file. No need to do anything.
                
            }
        }
    }
}
