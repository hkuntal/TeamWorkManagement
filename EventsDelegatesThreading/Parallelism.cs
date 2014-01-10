using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesThreading
{
    public class Parallelism
    {
        private static int Sum(int n)
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

        public static void InvokeMethodsParallely()
        {

            try
            {
                Parallel.Invoke(() => Console.WriteLine(Sum(21346)),
                                () => Console.WriteLine(Sum(21346123)),
                                () => Console.WriteLine(Sum(213434))
                    );
            }
            catch (AggregateException ex)
            {
                HandleException(ex);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static void HandleException(AggregateException ex)
        {
            foreach (var x in ex.InnerExceptions)
            {
                Console.WriteLine(x.Message);
            }
        }

    }
}
