using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    internal static class MulitplesOf3And5
    {
        public static int Solve(int maxValue)
        {
            //int i = 1;
            int sum = 0;
            for (int i = 1; i < maxValue; i++)
            {
                if (i%3 == 0 || i%5 == 0)
                {
                    sum = sum + i;
                }
            }

            //while (3 * i < maxValue)
            //{
            //    sum += 3*i;
            //    i++;
            //}
            //i = 1;
            //while (5 * i < maxValue)
            //{
            //    sum += 5 * i;
            //    i++;
            //}
            return sum;
        }

        public static int FibonacciProblem2()
        {
            int a = 1, b = 1;
            int c = 0, sum = 0;
            while (c < 4000000)
            {
                c = a + b;
                a = b;
                b = c;
                if (c%2 == 0)
                {
                    sum += c;
                }
            }
            return sum;
        }

        public static long LargestPrimeFactor(long a)
        {
            int lpn = 1;
            long largestPrimeFactor=1;
            for (long i = a/2; i > 1; i--)
            {
                if (a%i == 0)
                {
                    if (i.IsPrime())
                    {
                        largestPrimeFactor = i;
                        return largestPrimeFactor;
                    }
                }
            }
            return largestPrimeFactor;
        }
    }

    public static class Extensions
    {
        public static bool IsPrime(this long number)
        {

            for (int i = 2; i < number; i++)
            {
                if (number%i == 0)
                    return false;
            }
            return true;
        }
    }
}
