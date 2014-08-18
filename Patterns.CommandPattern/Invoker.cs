using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.CommandPattern
{
    //Invoker class that is actually going to hold the command object and later execute the command based on certain actions
    class SimpleRemoteControl
    {
        private ICommand slot;

        public SimpleRemoteControl()
        {
            
        }
        public void SetCommand(ICommand command)
        {
            slot = command;
        }
        public void ButtonWasPressed()
        {
            slot.Execute();
        }
    }

    class RemoteControl
    {
        //Create the multiple commands
        private ICommand[] onCommands;
        private ICommand[] offCommands;

        public RemoteControl()
        {
            offCommands = new ICommand[7];
            onCommands = new ICommand[7];
            //Populate the command array with NO commands as of now
            for (int i = 0; i < 7; i++)
            {
                offCommands[i] = new NoCommad();
                onCommands[i] = new NoCommad();
            }

        }
        //Set teh commands as per the slot
        public void SetCommand(int slot, ICommand offCommand, ICommand onCommand)
        {
            onCommands[slot] = offCommand;
            offCommands[slot] = onCommand;
        }
        public void OnButtonWasPused(int slot)
        {
            onCommands[slot].Execute();
        }
        public void OffButtonWasPused(int slot)
        {
            offCommands[slot].Execute();
        }

    }
}
