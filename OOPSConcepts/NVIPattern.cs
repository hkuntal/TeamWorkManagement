using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOPSConcepts.NormalPattern
{
    public class Base
    {
        public virtual void DoWork()
        {
            Console.WriteLine("Base.DoWork()");
        }
    }

    public class Derived : Base
    {

        public override void DoWork()
        {
            Console.WriteLine("Derived.DoWork()");
        }
    }

    public class EntryPoint
    {
        public static void Main1()
        {
            Base b = new Derived();
            b.DoWork();
        }
    }
}
//NVI Pattern comes here in picture. Read the text from the book C# 2010

/*However, the design could be subtly more robust. Imagine that you’re the writer of Base, and you
have deployed Base to millions of users. Many people are happily using Base all over the world when you
decide, for some good reason, that you should do some pre- and postprocessing within DoWork. For
example, suppose thatyou would like to provide a debug version of Base that tracks how many times the
DoWork method is called. As the code was written previously, you cannot do such a thing without forcing
breaking changes onto the millions of users who have used your class Base. For example, you could
introduce two more methods, named PreDoWork and PostDoWork, and ask kindly that your users
reimplement their overrides so that they call these methods at the correct time. Ouch! Now, let’s
consider a minor modification to the original design that doesn’t even change the public interface of
Base:
 THIS IS ACTUALLY SIMILAR TO TEMPLATE METHOD PATTERN
 */

namespace OOPSConcepts.NVIPattern
{
    public class Base
    {
        public void DoWork()
        {
            //Add any preprocessing code here
            Console.WriteLine("This is preprocessing code");
            CoreDoWork();
            //Add any post processing code here
            Console.WriteLine("This is postprocessing code");
        }
        protected virtual void CoreDoWork()
        {
            Console.WriteLine("Base.DoWork()");
        }
    }

    public class Derived : Base
    {

        protected override void CoreDoWork()
        {
            Console.WriteLine("Derived.DoWork()");
        }
    }

    public class EntryPoint
    {
        public static void Main1()
        {
            Base b = new Derived();
            b.DoWork();
        }
    }
}

