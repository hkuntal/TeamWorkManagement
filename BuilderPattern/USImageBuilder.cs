using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class USImageBuilder:IImageBuilder
    {
        IImage img = new Image();

        public void GenerateSopClass()
        {
            // The logic can be very specific to the Ultrasound images
            img.SopClass = "US1.2.3.4";
        }

        public IImage GetImage()
        {
            return img;
        }

        public void SetTheModality()
        {
            // This logic can also be very specific to ultrasound
            img.Modality = "US";
        }
    }
}
