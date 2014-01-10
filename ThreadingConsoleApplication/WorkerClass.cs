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
    }
}
