using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceWithThreads
{
    internal class Program
    {
        private static object syncKey = new object();

        private static void Main(string[] args)
        {
            Console.WriteLine("Invoking the delegate");
            var t = RunWithTimeSpan(DoSomethingWihtLock);

            var y = RunWithTimeSpan(DoSomethingWihtLock);
            Console.WriteLine("Time taken to execute the method with lock" + t.TimeSpan);
            Console.WriteLine("Time taken to execute the method with lock" + y.TimeSpan);
            Console.ReadLine();
        }

        private static int DoSomethingWihtOutLock()
        {
            int a = 5;
            for (int i = 0; i < 100000; i++)
            {
                //Console.WriteLine(i);
                a += 5;
            }
            return a;
        }

        private static int DoSomethingWihtLock()
        {
            int a = 5;
            for (int i = 0; i < 100000; i++)
            {
                lock (syncKey)
                {
                    a += 5;
                    //Console.WriteLine(i);
                }
            }
            return a;
        }

        public static TimeSpanWithValue<T> RunWithTimeSpan<T>(Func<T> function)
        {
            var timeSpanWithValue = new TimeSpanWithValue<T>();
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            timeSpanWithValue.Value = function();
            stopwatch.Stop();
            timeSpanWithValue.TimeSpan = stopwatch.Elapsed;
            return timeSpanWithValue;
        }
    }

    public class TimeSpanWithValue<T>
    {
        public TimeSpan TimeSpan { get; set; }

        public T Value { get; set; }
    }

}
