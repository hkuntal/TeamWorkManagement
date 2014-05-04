using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator.Main
{
    /*This pattern is all about giving new responsibilites to objects without making any code changes to the underlying classes
     1. Decorators should have the same type they decorate
     2. 
     
     */
    class Program
    {
        static void Main(string[] args)
        {
            //create an object of the basic class
            Beverage objBeverage = new Espresso();
            //print its cost
            Console.WriteLine(objBeverage.getDescription() + "$" + objBeverage.cost());
            // Decorate it with a condiment
            CondimentDecorator objMocha = new Mocha(objBeverage);
            Console.WriteLine(objMocha.getDescription() + "$" + objMocha.cost());

            //Decorate it with Whip
            CondimentDecorator objWhip = new Whip(objMocha);
            Console.WriteLine(objWhip.getDescription() + "$" + objWhip.cost());
            Console.ReadLine();
        }
    }
}
