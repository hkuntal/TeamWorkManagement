using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace UnHandledException
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //CheckUnhandledExceptionAtAppDomainLevel();
            CheckIfForegroundThreadWillThrowException();
        }

        private static void GlobalUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = default(Exception);
            ex = (Exception) e.ExceptionObject;
            LogError(ex);
            //ILog log = LogManager.GetLogger(typeof(Program));
            //log.Error(ex.Message + "\n" + ex.StackTrace);
        }

        private static void CheckUnhandledExceptionAtAppDomainLevel()
        {
            try
            {
                AppDomain currentDomain = default(AppDomain);
                currentDomain = AppDomain.CurrentDomain;
                // Handler for unhandled exceptions.
                currentDomain.UnhandledException += GlobalUnhandledExceptionHandler;
                // Handler for exceptions in threads behind forms.
                //System.Windows.Forms.Application.ThreadException += GlobalThreadExceptionHandler;
                // USING TASKS
                Task t = new Task(ExceptionMethod);
                t.Start();
                t.Wait();

                //USING THREADS
                //var t= new Thread(ExceptionMethod);
                //t.IsBackground = true;
                //t.Start();

                //ExceptionMethod();
            }
            //NOTE: When using a Task object the Aggregate exception is thrown but we need to catch it explicitly and deal with it by checking into its innter exceotions
            //properties. Also note that Aggregate exception is thrown only when we request wait or result that is when we write task.Result() or task.Wait()

                // When we are using Threads
            //With Background threads the App Domain Unhandle Exception is not thrown. The thread has to be foreground in that case. If it is foreground than the 
            //AppDomain Unhandled exception is thrown

                //catch (Exception ex)
            //{
            //    LogError(ex);
            //}
            finally
            {

            }
        }

        private static void CheckIfForegroundThreadWillThrowException()
        {
            try
            {
                var CleanupThread = new Thread(RunCacheCleanup) {Name = "CleanupThreadName"};
                CleanupThread.Start();
            
            }
            catch (Exception ex)
            {
                //NOTE: This exception block will not catch the exceptions from the called threads 
                Console.WriteLine(ex.Message);
            }
        }

        private static void RunCacheCleanup()
        {
            ExceptionMethod();
        }

        private static void LogError(Exception ex)
        {
            using (var sw = new StreamWriter(@"C:\Hariom\UnhandleException.txt"))
            {
                var a = ex as AggregateException;
                if (a != null)
                {
                    var col = a.InnerExceptions;
                    foreach (Exception exception in col)
                    {
                        sw.Write(exception.Message + "\n" + exception.StackTrace);
                    }
                }
                else
                {
                    sw.Write(ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        private static void GlobalThreadExceptionHandler(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = default(Exception);
            ex = e.Exception;
            //ILog log = LogManager.GetLogger(typeof (Program)); //Log4NET
            //log.Error(ex.Message + "\n" + ex.StackTrace);

        }

        private static void ExceptionMethod()
        {
            Console.WriteLine("Throwing an exception");
            throw new DivideByZeroException();
        }
    }
}

