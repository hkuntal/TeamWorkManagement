using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EventsDelegatesThreading
{
    public class ThreadingTest
    {
        private static string value =
            "This is a string available to the parent thread. Needs to check if this will be available to the children threads";

        //NOTE: Yes this is available by default
        public void TestThreadPool()
        {
            try
            {
                CallContext.LogicalSetData("name", "ruby");
                //Pool a method via the threadpool
                ThreadPool.QueueUserWorkItem(ComputeBoundOp, "hariom");
                //Another way of doing this
                ExecutionContext.SuppressFlow();
                //This will not allow the flowing of the ThreadContext data to the child thread
                WaitCallback obj = new WaitCallback(ComputeBoundOp);
                ThreadPool.QueueUserWorkItem(obj, "kuntal");
                //Anotehr way
                WaitCallback obj1 = new WaitCallback(k =>
                    {
                        Console.WriteLine("In ComputeBounndOp method with state {0}", k);
                        Thread.Sleep(5000);
                    });
                ThreadPool.QueueUserWorkItem(obj1, "singh");
                Console.WriteLine("Main thread: Doing other work here...");
                Thread.Sleep(5000); // Simulating other work (10 seconds)
                Console.WriteLine("Hit <Enter> to end this program...");
                Console.ReadLine();
                //Not you cannot do any kind of exception handling with QueueUserWorkItem with threadpool Queue UserWork Item. For that you need to use Tasks
            }
            catch (Exception ex)
            {
                //NOTE: An exception thrown from the child thread do not reach the parent thread. CLR closes the child thread after displaying a message box that application has stopped working, 
                //but the parent thread continues to run
                Console.WriteLine("Received an exception from the called thread: {0}", ex.Message);
            }

        }

        public void CreateANewThreadAndTest()
        {
            try
            {
                Thread t = new Thread(ComputeBoundOp1, 5);

                //Back ground thread will end as soon as the parent thread finishes execution. By default they are foreground threads.
                t.IsBackground = true;

                t.Start();
                Thread.Sleep(2000);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ComputeBoundOp(object state)
        {
            try
            {

                Console.WriteLine("In ComputeBounndOp method with state {0}", state);
                Console.WriteLine(value);
                //Display the data set in the parent thread context
                Console.WriteLine("Date received from the context. Name = {0}", CallContext.LogicalGetData("name"));
                Thread.Sleep(2000);

                //Check what happens if you throw an exception with no try catch block in the calling method
                throw new Exception("Throwing test exception");
            }
            catch
            {
                //Eat any exception thrown
            }
        }

        private static void ComputeBoundOp1(object divisor)
        {
            try
            {
                Console.WriteLine("In ComputeBounndOp method with state {0}", divisor);
                //Console.WriteLine(value);

                //Will throw divide by zero exception


                Console.WriteLine("Thread wait started - ");
                int a = 1000/Convert.ToInt32(divisor);
                Thread.Sleep(5000);
                Console.WriteLine("Thread wait End - ");
                //Console.ReadLine();
                //Check what happens if you throw an exception with no try catch block in the calling method
                //throw new Exception("Throwing test exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ComputeBound1 - " + ex.Message);
                //This is the top most method in the thread. If exception is not handled at this level, the thread and the application both will terminate.
                //throw; You cannot throw at this level as there is nothing to catch anything
            }
        }
    }

    internal static class CancellationDemo
    {
        //NOTE: Operations can be cancelled through the use of Cancellation Token sources
        public static void Go()
        {
            CancellationTokenSource objCancellationTokenSource = new CancellationTokenSource();

            ThreadPool.QueueUserWorkItem(o => Count(objCancellationTokenSource.Token, 1000));

            Console.WriteLine("Press <Enter> to cancel the operation.");
            Console.ReadLine();
            objCancellationTokenSource.Cancel(); // If Count returned already, Cancel has no effect on it
            // Cancel returns immediately, and the method continues running here...
            //Cencel the token
            //objCancellationTokenSource.Token.Register(); //We can register call backs to be executed when a token is being cancelled.
            Console.WriteLine("The main thread is back");
            Console.ReadLine();
        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (Int32 count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break; // Exit the loop to stop the operation
                }
                Console.WriteLine(count);
                Thread.Sleep(200); // For demo, waste some time
            }
            Console.WriteLine("Count is done");
        }
    }

    public static class TestTask
    {
        public static void CreateAndTestATask()
        {

            //Create a task
            //Task obj1 = new Task();
            Task<int> objTask = new Task<int>(n => Sum((int) n), 123456645);
            Task<int> objTask1 = new Task<int>((m) => Divide((int) m), 0);
            objTask.Start();
            objTask1.Start();

            //Aggregeate exception is not thrown for both the tasks but only for the task for which the exception occurs first. To receive aggregate exception for both the tasks 
            // we need to put each one of them separately in a finally block.
            try
            {
                Thread.Sleep(2000);
                Console.WriteLine(objTask1.Result);
                Console.WriteLine(objTask.Result);
            }

            catch (AggregateException ex)
            {
                ex.Handle(x =>
                    {
                        Console.WriteLine(x.Message);
                        return true;
                    });
            }
            //try
            //{
            //    Console.WriteLine(objTask1.Result);
            //}

            //catch (AggregateException ex)
            //{
            //    ex.Handle(x =>
            //    {
            //        Console.WriteLine(x.Message);
            //        return true;
            //    });
            //}
            Console.ReadLine();
        }

        public static void CreateParentChildTasks()
        {
            try
            {
                //NOTE: By default the child tasks that a parent task creates are independent of its creator
                Task<Int32[]> parent = new Task<Int32[]>(() =>
                    {
                        var results = new Int32[3]; // Create an array for the results
                        // This tasks creates and starts 3 child tasks
                        new Task(() => results[0] = Sum(1000000000), TaskCreationOptions.AttachedToParent).Start();
                            // Aggregate exception is not thrown if it is not attached to parent. Displays default value '0' in this case.
                        new Task(() => results[1] = Sum(1000000000), TaskCreationOptions.AttachedToParent).Start();
                        new Task(() => results[2] = Sum(30000), TaskCreationOptions.AttachedToParent).Start();
                        // Returns a reference to the array (even though the elements may not be initialized yet)

                        //throw new Exception("Just testing aggergate exception");
                        return results;
                    });
                // When the parent and its children have run to completion, display the results
                //If one of the child taks throws an exception say an overflow exception in this case, no results are displayed for the other child tasks.
                var cwt = parent.ContinueWith(
                    parentTask => Array.ForEach(parentTask.Result, Console.WriteLine));

                //This piece of code actually calls the exception handler below. The Aggregate exception will contain the exceptions for each of the child tasks.
                parent.ContinueWith(ExceptionHandler,
                                    TaskContinuationOptions.OnlyOnFaulted);

                // Start the parent Task so it can start its children
                parent.Start();
                //The below catch block executes only when we use parent.Wait() or parent.result() as seen below, otherwise the aggregate exception is never caught. Also aggregate exception occuring because of
                //child taks will only come if it atatched to parent, else it will not come, true for both catch block and exception handler method. Again the aggregate exception will contain the exception
                //for each of the child tasks.
                parent.Wait();

            }
            catch (AggregateException ex)
            {

                ex.Handle(x =>
                    {
                        Console.WriteLine("Inside the catch block : " + x.Message);
                        //If I return false here saying the exception was not handle than the application will be terminated.
                        return true;
                    });
            }
        }

        private static void ExceptionHandler(Task<Int32[]> task)
        {
            var exception = task.Exception;
            Console.WriteLine("Exceltion Handler Method: " + exception.Message);
        }

        public static void TestTaskAndExceptions()
        {
            try
            {

                Task<int> obj = new Task<int>(() => Sum(123565484));
                obj.Start();
                Task<int> obj1 = new Task<int>(() => Sum(123565484));
                obj1.Start();
                //var a = obj.Result;
                //Wait for the task to complete
                //obj.Wait();
                //Test for any exceptions if not then display the results
                //Thread.Sleep(2000);
                if (obj.Status != TaskStatus.Faulted)
                {
                    Console.WriteLine(obj.Result);
                    Console.WriteLine(obj1.Result);
                }
                else
                {
                    HandleException(obj.Exception);
                }

            }
            catch (AggregateException ex)
            {
                HandleException(ex);
            }
        }

        private static void HandleException(AggregateException ex)
        {
            foreach (var x in ex.InnerExceptions)
            {
                Console.WriteLine(x.Message);
            }
        }

        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
                checked
                    //The checked keyword is used to explicitly enable overflow checking for integral-type arithmetic operations and conversions.
                {
                    sum += n;
                } // in n is large, this will throw System.OverflowException
            return sum;
        }

        private static Int32 Sum(CancellationToken ct , Int32 n)
        {
            Thread.Sleep(1000);
            Int32 sum = 0;
            for (; n > 0; n--)
            {
                // The following line throws OperationCanceledException when Cancel
// is called on the CancellationTokenSource referred to by the token
                //sleep for sometime so that token gets cancelled
                //Thread.Sleep(1);
                //ct.ThrowIfCancellationRequested(); //this doesnt get caught below
                //throw new Exception("Hariom"); - Gets caught in the parent task on defaulted
                if (ct.IsCancellationRequested)
                    throw new DivideByZeroException("Hariom");//This hets caught below but not that InvalidOperationException();
                checked
                {
                    sum += n;
                } // in n is large, this will throw System.OverflowException
            }
            return sum;
        }

        private static Int32 Divide(Int32 n)
        {
            return 123456/n;
        }

        public static void TestTaskFactory()
        {
            Task parent = new Task(() =>
                {
                    var cts = new CancellationTokenSource();
                    var tf = new TaskFactory<Int32>(cts.Token,
                                                    TaskCreationOptions.AttachedToParent, //The parent task will fault only if it is attached to the children
                                                    //TaskCreationOptions.None,
                                                    TaskContinuationOptions.ExecuteSynchronously,
                                                    TaskScheduler.Default);
                    // This tasks creates and starts 3 child tasks
                    var childTasks = new[]
                        {
                            tf.StartNew(() => Sum(cts.Token, 10000)),
                            tf.StartNew(() => Sum(cts.Token, 20000)),
                            //tf.StartNew(() => Sum(cts.Token, Int32.MaxValue)) // Too big, throws OverflowException
                            tf.StartNew(() => Sum(cts.Token, 3000))
                        };

                    // If any of the child tasks throw, cancel the rest of them
                    for (Int32 task = 0; task < childTasks.Length; task++)
                        childTasks[task].ContinueWith(
                            t => cts.Cancel(), TaskContinuationOptions.OnlyOnFaulted);

                    //Hariom - instead of cancelling try to log the error
                    for (Int32 task = 0; task < childTasks.Length; task++)
                        childTasks[task].ContinueWith(
                            t => HandleException(t.Exception), TaskContinuationOptions.OnlyOnFaulted);

                    // When all children are done, get the maximum value returned from the
                    // non-faulting/canceled tasks. Then pass the maximum value to another
                    // task which displays the maximum result
                    tf.ContinueWhenAll(
                        childTasks,
                        completedTasks => completedTasks.Where(
                            t => !t.IsFaulted && !t.IsCanceled).Max(t => t.Result),
                        CancellationToken.None)
                      .ContinueWith(t => Console.WriteLine("The maximum is: " + t.Result),
                                    TaskContinuationOptions.ExecuteSynchronously);

                    //After waiting for 5 seconds cancel all the tasks and check if the operation cancelled exception is thrown
                    //Thread.Sleep(5000);
                    cts.Cancel();
                });
            // When the children are done, show any unhandled exceptions too
            parent.ContinueWith(p =>
                {
                    //NOTE: For some reason the exception thrown by ct.ThrowIfCancellationRequested() is not making this 
                    //delegate to execute, but it executes for other exceptions like Overflow or If I throw my own exception

                    // I put all this text in a StringBuilder and call Console.WriteLine just once
                    // because this task could execute concurrently with the task above & I don't
                    // want the tasks' output interspersed
                    StringBuilder sb = new StringBuilder(
                        "The following exception(s) occurred:" + Environment.NewLine);
                    foreach (var e in p.Exception.Flatten().InnerExceptions)
                        sb.AppendLine(" " + e.GetType().ToString());
                    Console.WriteLine(sb.ToString());
                }, TaskContinuationOptions.OnlyOnFaulted);
            // Start the parent Task so it can start its children
            parent.Start();
        }

        public static void TestTaskFactoryCreationAndExceptionHandling()
        {
            try
            {
                //Both cases work here

                //Case 1:
                var t = Task.Factory.StartNew(() => Console.WriteLine(Sum(1254623355)));
                //Thread.Sleep(2000);
                //t.ContinueWith(c => HandleException(c.Exception));
                //t.Wait();
            //Case 2:
            //var t = Task<int>.Factory.StartNew(() => Sum(1254623355));
            //Console.WriteLine(t.Result);
            }
            catch (AggregateException ex)
            {
                HandleException(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}



