using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingSmallStuff
{
    [TestClass]
    public class TestYield
    {
        [TestMethod]
        public void WithYield()
        {
            var somenumbers = System.Linq.Enumerable.Range(1, 10);

            var evens = GetEvenNumbers(somenumbers);

            foreach (var even in evens)
            {
                Debug.WriteLine("Output:" + even);
                if (even == 6)
                {
                    break;
                }
            }
        }

        private IEnumerable<int> GetEvenNumbers(System.Collections.Generic.IEnumerable<int> somenumbers)
        {
            foreach (var somenumber in somenumbers)
            {
                if (somenumber%2 == 0)
                {
                    Debug.WriteLine("Yielding number:" + somenumber);
                    yield return somenumber;
                }
            }
        }

        [TestMethod]
        public void TestYielding()
        {
            //var somenumbers = System.Linq.Enumerable.Range(1, 10);

            var evens = GetNumbers();

            foreach (var even in evens)
            {
                Debug.WriteLine("Output:" + even);
                //if (even == 6)
                //{
                //    break;
                //}
            }
        }

        private IEnumerable<int> GetNumbers()
        {
            //NOTE: "Yield break" will break out of the entire method when encountered. Break will just break out of the loop
            //NOTE2: it doesn’t really end the method’s execution. yield return pauses the method execution and the next time you call it (for the next enumeration value), the method will continue to 
            //execute from the last yield return call.

            for (int s = 16; s < 147; ++s)
            {
                if (s == 27) { yield break; }
                else if (s > 17) { yield return s; }
            }

            for (int i = 0; i < 10; ++i)
            {
                if (i > 5) { break; }
                yield return i;
            }

            for (int v = 2710; v < 2714; v += 2)
            {
                yield return v;
            }

            
        }
        
    }
}
