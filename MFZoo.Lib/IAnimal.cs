using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MFZoo.Lib
{
    public interface IAnimal
    {
        string Name { get; }
        Func<string, string> GiveMeFood { get; set; }

    }
}
