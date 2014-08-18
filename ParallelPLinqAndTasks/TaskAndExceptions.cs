using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelPLinqAndTasks
{
    class TaskAndExceptions
    {
        public static void CreateTaskAndCheckForExceptions()
        {
            try
            {
                //crtarte a task
                Task<int> objTask = new Task<int>(() => Sum(123546546));
                Task<int> objTask1 = new Task<int>(() => Sum(1235));
                objTask.Start();
                objTask1.Start();

                //NOTE: IF WE CALL THE TASK.RESULT AND AN EXCEPTION OCCURS ON THAT PARTICULAR TASK ONLY THEN AN EXCEPTION IS THROWN. UNCOMMENT THE BELOW LINES TO VERIFY THAT
                //Console.WriteLine(objTask1.Result);
                //Console.WriteLine(objTask.Result);

                //objTask.Dispose();
                
                //NOTE: If we wait on the Task then also the exception gets thrown
                //objTask.Wait(); //Throws the exception when called
                //objTask1.Wait(); //Does not throw exception in this case
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void CreateTasksViaTaskFactoryAndCheckForExceptions()
        {

            try
            {
                var errorTask = Task.Factory.StartNew(() => Sum(123546546));
                var noErroTask = Task.Factory.StartNew(() => Sum(1235));

                //Task.WaitAll();//Does not raise exceptions and program continues to execute
                //Task.WaitAll(errorTask);//Raises exceptions
                //Task.WaitAll(noErroTask);//Does not raise exception
                Task.WaitAll(errorTask, noErroTask); //raise exceptions

                //while (true)
                //{
                //    Console.WriteLine(errorTask.Status);
                //    Task.WaitAll(errorTask, noErroTask);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void CreateTasksViaTaskFactoryAndCheckForExceptionsAfterBeingHandledInDelegateItself()
        {
            try
            {
                var errorTask = Task.Factory.StartNew(() => SumWithExceptionHandling(123546546));
                var noErroTask = Task.Factory.StartNew(() => SumWithExceptionHandling(1235));

                Task.WaitAll(errorTask, noErroTask);

                Console.WriteLine(errorTask.Result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int Sum(Int32 n)
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

        private static int SumWithExceptionHandling(Int32 n)
        {
            Int32 sum = 0;
            try
            {

            for (; n > 0; n--)
                checked
                //The checked keyword is used to explicitly enable overflow checking for integral-type arithmetic operations and conversions.
                {
                    sum += n;
                } // in n is large, this will throw System.OverflowException

            }
            catch (Exception ex)
            {
                sum = -1;
            }
            return sum;
        }

    }
}
