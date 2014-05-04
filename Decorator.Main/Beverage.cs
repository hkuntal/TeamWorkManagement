using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator.Main
{
    //In place of an abstract class we could have used an interface as well
    public abstract class Beverage
    {
        public String description = "Unknown Beverage";

        public String getDescription()
        {
            return description;
        }

        public abstract double cost();
    }


    //The decorator should have the same class it decorates
    public abstract class CondimentDecorator : Beverage
    {
        public abstract String getDescription();
    }

    //Concrete Bevarage
    public class Espresso : Beverage
    {
        public Espresso()
        {
            description = "Espresso";
        }

        public override double cost()
        {
            return 1.99;
        }
    }

    public class HouseBlend : Beverage
    {
        public HouseBlend()
        {
            description = "House Blend Coffee";
        }

        public override double cost()
        {
            return .89;
        }
    }

    public class Mocha : CondimentDecorator
    {
        //Holds an instance of the class that has to be decorated
        private Beverage beverage;

        public Mocha(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override String getDescription()
        {
            return beverage.getDescription() + ", Mocha";
        }

        public override double cost()
        {
            return .20 + beverage.cost();
        }
    }
    public class Whip : CondimentDecorator
    {
        //Holds an instance of the class that has to be decorated
        private Beverage beverage;

        public Whip(Beverage beverage)
        {
            this.beverage = beverage;
        }

        public override String getDescription()
        {
            return beverage.getDescription() + ", Whip";
        }

        public override double cost()
        {
            return .70 + beverage.cost();
        }
    }
}
