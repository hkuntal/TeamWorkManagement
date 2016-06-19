using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            var objManufacturer = new Manufacturer();
            IList<IImage> list = new List<IImage>();
            // Provide an instance of the Builder object here. Builder object will construct the product and then you can access the product here
            IImageBuilder bld = new CTImageBuilder();

            objManufacturer.ConstructImage(bld);
            list.Add(bld.GetImage());
            
            // Use the another builder
            bld = new USImageBuilder();
            objManufacturer.ConstructImage(bld);
            list.Add(bld.GetImage());

            // Print the images to the console
            foreach (var image in list)
            {
                Console.WriteLine(image);
            }

            Console.ReadLine();
        }
    }
}
