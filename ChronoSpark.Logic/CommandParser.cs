using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public class CommandParser
    {
        readonly IEnumerable<ICommandFactory> availableCommands;

        public CommandParser(IEnumerable<ICommandFactory> availableCommands) 
        {
            this.availableCommands = availableCommands;
        }

        public ICommand ParseCommand(String readCommand)
        {
            
            var requestedCommandName = readCommand;
            var command = FindRequestedCommand(requestedCommandName);
            if (command != null)
            {
                return command.MakeCommand();
            }
            else { return null; }
        }  
         
        ICommandFactory FindRequestedCommand(String commandName)
        {
            return availableCommands
                .FirstOrDefault(cmd => cmd.CommandName == commandName);
        }
    }
}
