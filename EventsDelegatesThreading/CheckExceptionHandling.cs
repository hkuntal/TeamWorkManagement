using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegatesThreading
{
    class CheckExceptionHandling
    {
        public void Method1()
        {
            try
            {
                Method2();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }

        private void Method2()
        {
            try
            {

            Method3();

            }
            catch (Exception ex)
            {
                //If we throw the way below we loose the stack trace information. The stack trace will now begin from here although the exceptions other detals remain the same.
                throw ex;

                //To avoid loosing the stack trace just use 
                //throw;
            }
        }

        private void Method3()
        {
            Method4();
        }

        private void Method4()
        {
            throw new NotImplementedException();
        }

    }
}
