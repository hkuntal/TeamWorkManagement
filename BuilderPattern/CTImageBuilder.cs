using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class CTImageBuilder:IImageBuilder
    {
        IImage img = new Image();
        public void GenerateSopClass()
        {
            img.SopClass = "CT1.2.3.4";
        }

        public void SetTheModality()
        {
            img.Modality = "CT";
        }

        public IImage GetImage()
        {
            return img;
        }
    }
}
