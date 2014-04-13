using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(MulitplesOf3And5.Solve(1000));

            //Console.WriteLine(MulitplesOf3And5.FibonacciProblem2());

            //Console.WriteLine(MulitplesOf3And5.LargestPrimeFactor(600851475143));

            //Console.WriteLine(MulitplesOf3And5.LargestPrimeFactor(1665));

            //Console.WriteLine(MulitplesOf3And5.LargestPrimeFactor(134));
            var date = DateTime.Now;
            Console.WriteLine(ProjectEuler.ProjectEulerProblems.Problem10(2000000));
            Console.WriteLine("Time Taken: " + (DateTime.Now - date).Seconds);
            
            Console.Read();
        }
    }
}
