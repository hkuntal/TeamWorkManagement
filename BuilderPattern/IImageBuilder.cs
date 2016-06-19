using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    interface IImageBuilder
    {
        void GenerateSopClass();
        void SetTheModality();
        IImage GetImage();
    }
}
