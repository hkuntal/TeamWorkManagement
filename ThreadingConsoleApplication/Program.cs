using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadingConsoleApplication
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    //Thread thread1 = new Thread(new ThreadStart(EntryPoint.ThreadFunc1));
        //    //thread1.IsBackground = true;
        //    //thread1.Start();

        //    //Thread thread2 = new Thread(new ThreadStart(EntryPoint.ThreadFunc2));
        //    //thread2.Start();
        //    //Console.WriteLine("Exiting Main Thread");

        //    //Testing the simple wait lock class
        //    WorkerClass objWorkerClass = new WorkerClass(); 
        //    Thread t1 = new Thread(new ThreadStart(objWorkerClass.DoSomeWork));

        //    Thread t2 = new Thread(new ThreadStart(objWorkerClass.DoSomeWork));
        //    //Start the first thread
        //    t1.Start();
        //    //Start the second thread
        //    t2.Start();
        //}
        public class EntryPoint
        {
            public static void ThreadFunc1()
            {
                try
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Exiting extra thread 1");
                }
                catch (ThreadAbortException ex)
                {
                    Console.WriteLine("Thread abort exception has been received");
                    Console.Read();
                }


            }
            public static void ThreadFunc2()
            {
                try
                {
                    Thread.Sleep(10000);
                    Console.WriteLine("Exiting extra thread 2");
                }
                catch (ThreadAbortException ex)
                {
                    Console.WriteLine("Thread abort exception has been received");
                    Console.Read();
                }


            }
        }




        static AutoResetEvent autoEvent = new AutoResetEvent(true);

        static void Main()
        {
            Console.WriteLine("Main starting.");

            ThreadPool.QueueUserWorkItem(
                new WaitCallback(WorkMethod), autoEvent);

            // Wait for work method to signal.
             bool b = autoEvent.WaitOne();
             Console.WriteLine(b.ToString());
            Console.WriteLine("Work method signaled.\nMain ending.");
            Thread.Sleep(10000);
        }

        static void WorkMethod(object stateInfo)
        {
            //Thread.Sleep(8000);
            Console.WriteLine("Work starting.");

            // Simulate time spent working.
            Thread.Sleep(new Random().Next(100, 2000));

            // Signal that work is finished.
            Console.WriteLine("Work ending.");
            bool c=((AutoResetEvent)stateInfo).Set();
            Console.WriteLine(c.ToString());
            Thread.Sleep(3000);
            Console.Read();
        }


    }
}
