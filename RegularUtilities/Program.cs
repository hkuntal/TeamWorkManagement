using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegularUtilities
{
    class Program
    {
        private static object syncKey = new object();
        static void Main(string[] args)
        {
            Console.WriteLine("Invoking the delegate");
            var t = RunWithTimeSpan(DoSomethingWihtOutLock);

            var y = RunWithTimeSpan(DoSomethingWihtOutLock);
            Console.WriteLine("Time taken to execute the method without lock" + t.TimeSpan);
            Console.WriteLine("Time taken to execute the method without lock" + y.TimeSpan);
            Console.ReadLine();
        }

        private static int DoSomethingWihtOutLock()
        {
            for (int i = 0; i < 10000000; i++)
            {
                Console.WriteLine(i);
            }
            return 0;
        }

        private static int DoSomethingWihtLock()
        {
            for (int i = 0; i < 10000000; i++)
            {
                lock (syncKey)
                {
                    Console.WriteLine(i);
                }
            }
            return 0;
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
