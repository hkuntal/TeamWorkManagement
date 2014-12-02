using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterDesignPattern
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Adapter design pattern....");

            // Create adapter and place a request
            Target target = new Adapter(); // You can also create a adaptee object and pass it in the constructor of the Adapter object
            target.Request();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// The 'Target' class
    /// </summary>
    /// This couls well be an interface or an abstract class
    internal class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("Called Target Request()");
        }
    }

    /// <summary>
    /// The 'Adapter' class
    /// </summary>
    internal class Adapter : Target
    {
        private Adaptee _adaptee = new Adaptee();

        public Adapter()
        {
            
        }

        public Adapter(Adaptee obj)
        {
            _adaptee = obj;
        }

        public override void Request()
        {
            // Possibly do some other work
            //  and then call SpecificRequest
            _adaptee.SpecificRequest();
        }
    }

    /// <summary>
    /// The 'Adaptee' class
    /// </summary>
    internal class Adaptee
    {
        public void SpecificRequest()
        {
            Console.WriteLine("Called SpecificRequest()");
        }
    }
}

