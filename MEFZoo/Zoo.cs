using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MFZoo.Lib;
using System.ComponentModel.Composition;
namespace MEFZoo
{
    class Zoo
    {
        [ImportMany(typeof(IAnimal))]
        public IEnumerable<IAnimal> Animals { get; set; }

        [Export("AnimalFood")]
        public string GiveFood(string animalType)
        {
            switch (animalType.ToLower())
            {
                case "herbivores":
                    return "GreenGrass";
                case "carnivores":
                    return "Readmeat";
                default:
                    return "Waste";
            }
        }
    }
}
