using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    class Client
    {
        //Simulate client API that is going to use the Command API
        public static void ExecuteCommandPattern()
        {
            //This is the Invoker that is used to set the command object and will call execute method on the command object.
            SimpleRemoteControl objSimpleRemoteControl = new SimpleRemoteControl();

            //Prepare the object on which the command is to be executed
            Light objLight = new Light();
            //Prepare teh command object that is going to work on this command
            LightsOnCommand objLightOnCommand = new LightsOnCommand(objLight);
            
            objSimpleRemoteControl.SetCommand(objLightOnCommand);
            objSimpleRemoteControl.ButtonWasPressed();

            //Switch of the button Off
            LightsOffCommand objLightsOffCommand = new LightsOffCommand(objLight);
            objSimpleRemoteControl.SetCommand(objLightsOffCommand);
            objSimpleRemoteControl.ButtonWasPressed();
        }

        public static void ExecuteCommandPatternInBulk()
        {
            //This is the Invoker that is used to set the command object and will call execute method on the command object.
            RemoteControl objRemoteControl = new RemoteControl();

            //Prepare the object on which the command is to be executed
            Light objLight = new Light();

            //Prepare the command object that is going to work on this command
            LightsOnCommand objLightOnCommand = new LightsOnCommand(objLight);

            //Switch of the button Off
            LightsOffCommand objLightsOffCommand = new LightsOffCommand(objLight);
            
            //Create the stereo object
            Stereo objStereo = new Stereo();
            StereoOnWithCDCOmmand objStereoOnWithCDCOmmand = new StereoOnWithCDCOmmand(objStereo);

            //Set the commands in the slot
            objRemoteControl.SetCommand(1,objLightOnCommand, objLightsOffCommand);

        }
    }
}
