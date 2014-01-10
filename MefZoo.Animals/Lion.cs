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
    class Lion:IAnimal
    {
        private Timer _t;
        public string Name
        {
            get { return "Gir Lion"; }
        }

        [Import("AnimalFood")]
        public Func<string, string> GiveMeFood { get; set; }

        public Lion()
        {
            _t = new Timer(2000);
            _t.Elapsed += (sender, args) =>
                {
                    string food = GiveMeFood("carnivores");
                    Console.WriteLine("Lion is eating" + food);
                };
            _t.Start();
        }

        
    }
}
