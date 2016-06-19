using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class Manufacturer
    {
        public void ConstructImage(IImageBuilder imgBuilder)
        {
            // calls the appropriate methods of the builder. Additional logic can be placed here depending on the type if builder
            imgBuilder.GenerateSopClass();
            imgBuilder.SetTheModality();
        }
    }
}
