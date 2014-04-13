using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace ProjectEuler
{
    public static class ProjectEulerProblems
    {
        public static int Problem4()
        {
            int largestPalindrome = 0;
            int k = 0;
            //return the largest palindrome for three digit number
            int j = 999;
            while (j > 99)
            {
                for (int i = 999; i >= 100; i--)
                {
                    k = i*j;
                    if (IsPalidrome(k))
                        largestPalindrome = k > largestPalindrome ? k : largestPalindrome;

                }
                j--;
            }
            return largestPalindrome;
        }
        public static bool IsPalidrome(int n)
        {
            string s = n.ToString();
            int l = s.Length;
            for (int i = 0; i < l/2; i++)
            {
                if (s[i] != s[l - i - 1])
                {
                    return false;
                }
                
            }
            return true;
        }

        static ConcurrentBag<Int64> primeList = new ConcurrentBag<Int64>();
        public static Int64 Problem10(int maxValue)
        {

            var range = Enumerable.Range(1, maxValue);
            //Use thread pool library to find the numbers
            //Using Parallel is faster than individual foreach loop
            Parallel.ForEach(range, AddToList);
            //foreach (int i in range)
            //{
            //    AddToList(i);
            //}
            
            //Add the numbers in the list
            primeList.Add(2);
            var sum = primeList.Sum();
            throw new DivideByZeroException();
            return sum;
        }
        private static void AddToList(int n)
        {
            if (n%2 == 0 || n==1)
                return;
            if (ProjectEuler.Extensions.IsPrime(n))
            {
                primeList.Add(n);
            }
        }
        public static void Problem11()
        {
            
        }
    }
}
