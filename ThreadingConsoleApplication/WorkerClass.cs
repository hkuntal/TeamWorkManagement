using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThreadingConsoleApplication
{
    class WorkerClass
    {
        SimpleWaitLock objSimpleWaitLock = new SimpleWaitLock(); 
        public void DoSomeWork()
        {
            //Acquire the Lock
            objSimpleWaitLock.Enter();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Do some work has been called by thread {0}", Thread.CurrentThread.ManagedThreadId);
            }
            Thread.Sleep(10000);
            objSimpleWaitLock.Leave();
        }

        public void DoSomeWorkWithAbortException()
        {
            // NOTE1: When the thread is aborted through Thread.CurrentThread.Abort(); no thread abort exception or any other exception is thrown to the
            // calling thread. 
            try
            {

                Console.WriteLine("DoSomeWorkWithAbortException has been called");
                //Thread.CurrentThread.Abort();
                //Thread.Sleep(5000);
                    // simulating sleep so it can be terminated. Sleeping thread can be terminated as well.

                //NOTE: trying to see if a finally block will be called if a thread is aborted
                SampleMethod();
            }
            catch (Exception ex)
            {
                // NOTE: Puttinh a try catch block here does that catch the exception here and logs it but it does not affect the parent thread in any way
                Console.WriteLine("DoSomeWorkWithAbortException Exception: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Finally block has been called");
            }
        }
        private static void SampleMethod()
        {
            try
            {

                Thread.Sleep(5000);
            }
            finally
            {
                Console.WriteLine("SampleMethod finally method has been called" );
                throw new DivideByZeroException();
            }
        }
    }
}
