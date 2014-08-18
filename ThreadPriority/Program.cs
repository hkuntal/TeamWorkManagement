using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPriorities
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityTest priorityTest = new PriorityTest();
            ThreadStart startDelegate =
                new ThreadStart(priorityTest.ThreadMethod);

            Thread threadOne = new Thread(startDelegate);
            threadOne.Name = "ThreadOne";
            Thread threadTwo = new Thread(startDelegate);
            threadTwo.Name = "ThreadTwo";

            threadTwo.Priority = ThreadPriority.BelowNormal;
            threadOne.Start();
            threadTwo.Start();

            // Allow counting for 10 seconds.
            Thread.Sleep(10000);
            priorityTest.LoopSwitch = false;
        }
    }
    class PriorityTest
    {
        bool loopSwitch;

        public PriorityTest()
        {
            loopSwitch = true;
        }

        public bool LoopSwitch
        {
            set { loopSwitch = value; }
        }

        public void ThreadMethod()
        {
            long threadCount = 0;

            while (loopSwitch)
            {
                threadCount++;
                Console.WriteLine("{0} with {1,11} priority " +
                "has a count = {2,13}", Thread.CurrentThread.Name,
                Thread.CurrentThread.Priority.ToString(),
                threadCount.ToString("N0"));
            }
            
        }
    }
}
