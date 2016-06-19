using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    class Image : IImage
    {
        public string Modality { get; set; }
        public string SopClass { get; set; }

        public override string ToString()
        {
            return Modality + " " + SopClass;
        }
    }
}
