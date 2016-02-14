using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            // Test static events and delagates. Yes, we can create static events and bind them to multiple event handlers - weather static or non static

            // create two batsman
            var sachin = new Batsman {Name = "Sachin"};
            var rahul = new Batsman {Name = "Dravid"};

            EventRaiser.ReceiveData += sachin.SwingTheBat;
            EventRaiser.ReceiveData += rahul.SwingTheBat;

            Console.WriteLine("Do you want to raise the event (y/n) ?");

            var ans = Console.ReadLine();

            if (ans == "y")
            {
                // raise the event
                EventRaiser.RaiseTheEvent();
            }

            Console.ReadLine();
        }

        public static class EventRaiser
        {
            // declare an static event 
            public static event EventHandler<BallEventArgs> ReceiveData;

            public static void RaiseTheEvent()
            {
                ReceiveData(null, new BallEventArgs{BallSpeed = "60"});
            }
        }

        public class Batsman
        {
            public string Name { get; set; }

            public void SwingTheBat(object sender, BallEventArgs e)
            {
                Console.WriteLine(Name + " hit the ball that came with the speed " + e.BallSpeed);
            }
        }
    }
}
