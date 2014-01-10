using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MFZoo.Lib;
using System.ComponentModel.Composition;

namespace MefZoo.Animals
{
    [Export(typeof(IAnimal))]
    class Tiger:IAnimal
    {
        private Timer _t;

        public string Name
        {
            get { return "Asian Tiger"; }
        }

        [Import("AnimalFood")]
        public Func<string, string> GiveMeFood { get; set; }

         public Tiger()
        {
            _t = new Timer(2000);
            _t.Elapsed += (sender, args) =>
                {
                    string food = GiveMeFood("herbivores");
                    Console.WriteLine("Tiger is eating" + food);
                };
            _t.Start();
        }

    }
}
