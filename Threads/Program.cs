using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            //NOTE: This is to check what happens when an exception is thrown inside a thread and inside the catch block of the parent method in the thread
            //ANS: 

            BackGroundDisposeManager.EnqueueForDispose("Hariom");

            Console.WriteLine("Waiting for 5 secs");
            //Thread.Sleep(5000);

            // Trying to dispose the object again on the thread
            //Console.WriteLine("Trying to dispose the object again on the thread");
            //BackGroundDisposeManager.EnqueueForDispose("Hariom");

            Thread.Sleep(5000);
            //After 5 secs see if the thread is still alive
            Console.WriteLine("Is Thread still alive: " + BackGroundDisposeManager.IsThreadAlive());
            Console.WriteLine("Ha ha ha ha");

            BackGroundDisposeManager.EnqueueForDispose("Hariom");

            Console.ReadLine();


        }
    }
    public static class BackGroundDisposeManager
    {
        //private static readonly Thread DeleteQueueThread = new Thread(DeletionRunner) { Name = "BackgroundDisposeManager_Global" };

        private static readonly Thread DeleteQueueThread = new Thread(DeletionRunnerWithExternalCatchBlock) { Name = "BackgroundDisposeManager_Global" };

        public static void EnqueueForDispose(string item)
        {
            // Check to make sure the deletion thread is running (and avoid static init).
            if (!DeleteQueueThread.IsAlive)
            {
                Console.WriteLine("\n Starting the thread");
                DeleteQueueThread.Start();
            }

        }

        private static void DeletionRunner()
        {
            try
            {
                while (true)
                {
                    IDisposable item;
                    
                        try
                        {
                            // Do some operations and throw an exception
                            Console.WriteLine("Doing some operations ");

                            Console.WriteLine("\n Throwing an exception now");

                            var a = 5;
                            var b = 10/(a - a);
                        }
                        catch (Exception e)
                        {
                            // Have to be very careful; don't want to leak resources!
                            Console.WriteLine("An Exception has occured of type  " + e.GetType() + "Message = " + e.Message);

                            //Simulating Logging exception in the catch block
                            //throw new Exception("A logging exception has occurred");
                            //Logger.LogError(LoggerEventId.DisposeManager, Utilities.GetExceptionMessage(e));
                        }
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread aborted.");
                // Goodbye!
            }
        }

        private static void DeletionRunnerWithExternalCatchBlock()
        {
            try
            {
                while (true)
                {
                    IDisposable item;

                    
                        // Do some operations and throw an exception
                        Console.WriteLine("Doing some operations ");

                        Console.WriteLine("\n Throwing an exception now");

                        var a = 5;
                        var b = 10 / (a - a);
                    
                    
                }
            }
            catch (ThreadAbortException)
            {
                Console.WriteLine("Thread aborted.");
                // Goodbye!
            }
            catch (Exception e)
            {
                // Have to be very careful; don't want to leak resources!
                Console.WriteLine("An Exception has occured of type  " + e.GetType() + "Message = " + e.Message);

                //Simulating Logging exception in the catch block
                //throw new Exception("A logging exception has occurred");
                //Logger.LogError(LoggerEventId.DisposeManager, Utilities.GetExceptionMessage(e));
            }
            
        }

        internal static bool IsThreadAlive()
        {
            return DeleteQueueThread.IsAlive;
            //throw new NotImplementedException();
        }
    }
}
