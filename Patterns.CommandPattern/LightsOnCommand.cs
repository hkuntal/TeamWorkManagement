using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    class LightsOnCommand:ICommand
    {
        //Need to accept an object on which the command will be fired
        public void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
