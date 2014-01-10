using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ToBeTested;

namespace NUnitTesting
{
    [TestFixture]
    public class MySampleClass
    {
        [Test]
        public void TestAdd()
        {
            Calculator obj = new Calculator();
            int result = obj.Add(3, 5);
            Assert.AreEqual(8, result);
        }
        public void TestMultiply()
        {
            Calculator obj = new Calculator();
            int result = obj.Add(3, 5);
            Assert.Fail("Multiplication has failed");
        }
        //[Test,ExpectedException(typeof(DivideByZeroException))]
        [Test]
        public void DivideByZeroExceptionTest()
        {
            Calculator obj = new Calculator();
            //decimal result = obj.Divide(5, 0);
            Assert.Throws<DivideByZeroException>(() => obj.Divide(2,2),"Trying to divide by zero...haha");
        }
        [Test,Ignore]
        public void IgnoreTest()
        {
            Calculator obj = new Calculator();
            decimal result = obj.Divide(5, 0);
        }
    }

}
