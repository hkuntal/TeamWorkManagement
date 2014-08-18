using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPLinqAndTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //FINDING: 
            //Parallels.CheckExceptionWithParallelForEach();

            //TaskAndExceptions.CreateTaskAndCheckForExceptions();

            TaskAndExceptions.CreateTasksViaTaskFactoryAndCheckForExceptions();

            //TaskAndExceptions.CreateTasksViaTaskFactoryAndCheckForExceptionsAfterBeingHandledInDelegateItself();

            //NOTE: GC.Collect is to try what Jeffery Ritcher says that if not Result or Wait is called on a Task and when the GC collects a Task object
            //and it sees that there were certain exceptions thrown wihtin the task that were not catched, the CLR will terminate the exception.
            //But this does not seem to work

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Console.ReadLine();
        }
    }
}
