using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    //This is actually a command object so it will implement the command interface. The client is responsiblr for creating the command object.
    //The command object consist of a set of actions on a receiver
    class LightsOnCommand:ICommand
    {
        //Object on which command is going to be performed, also called as the RECEIVER
        private Light _light;

        public LightsOnCommand(Light light)
        {
            _light = light;
        }
        //Need to accept an object on which the command will be fired
        public void Execute()
        {
            _light.On();
        }
    }

    class LightsOffCommand : ICommand
    {
        //Object on which command is going to be performed, also called as the RECEIVER
        private Light _light;

        public LightsOffCommand(Light light)
        {
            _light = light;
        }
        //Need to accept an object on which the command will be fired
        public void Execute()
        {
            _light.Off();
        }
    }

    internal class NoCommad : ICommand
    {
        public void Execute()
        {

        }

    }
    class StereoOnWithCDCOmmand:ICommand
    {
        private Stereo _stereo;
        public StereoOnWithCDCOmmand(Stereo st)
        {
            _stereo = st;
        }

        public void Execute()
        {
            _stereo.On();
        }
    }
}
