using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpExamplesToBeUnitTested;

namespace TestingSmallStuff
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int a = 5;
            int b = 10;
            int c = a*b;
            Assert.AreEqual(50,c);
        }

        [TestMethod]
        public void TestIsPrime()
        {
            long a = 3571;
            bool b = ProjectEuler.Extensions.IsPrime(a);
            Assert.AreEqual(true, b);
        }

        [TestMethod]
        public void TestNumberStyles()
        {
            int a = int.Parse("(24)", NumberStyles.AllowParentheses);
            System.Diagnostics.Debug.WriteLine(a);
        }

        [TestMethod]
        public void ParseDateTime()
        {
            string date = "01/12/2013";
            var d = DateTime.Parse(date);//considers the date as 12 Jan 2013
            System.Diagnostics.Debug.WriteLine(d.ToLongDateString());

            var e = DateTime.ParseExact(date, "dd/MM/yyyy", null);//considers the date as 1 Dec 2013
            System.Diagnostics.Debug.WriteLine(e.ToLongDateString());
        }
        [TestMethod]
        public void IsPalindrome()
        {
            Assert.AreEqual(true, ProjectEuler.ProjectEulerProblems.IsPalidrome(905609));
        }
        [TestMethod]
        public void Problem4()
        {
            System.Diagnostics.Debug.WriteLine(ProjectEuler.ProjectEulerProblems.Problem4().ToString());
            //Assert.AreEqual(true, ProjectEuler.ProjectEuler.Problem4());
        }

        [TestMethod]
        public void BaseChildClassConstructorCalls()
        {
            CSharpExamplesToBeUnitTested.Child obj = new Child(5);
            
        }
        
    }
}
