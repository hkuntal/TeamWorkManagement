using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderPattern
{
    interface IImage
    {
        string Modality { get; set; }
        string SopClass { get; set; }
    }
}
