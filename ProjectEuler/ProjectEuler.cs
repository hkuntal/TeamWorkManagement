using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class ProjectEuler
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
        
    }
}
