using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    //All command objects are going to implement this interface, with just one method which is "Execute"
    interface ICommand
    {
        void Execute();
    }
}
