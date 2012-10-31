using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    class CommandParser
    {
        readonly IEnumerable<ICommandFactory> availableCommands;

        public CommandParser(IEnumerable<ICommandFactory> availableCommands) 
        {
            this.availableCommands = availableCommands;
        }

        internal ICommand ParseCommand(String[] args)
        {
            var requestedCommandName = args[0];
            var command = FindRequestedCommand(requestedCommandName);
            return command.MakeCommand();
        }  
         
        ICommandFactory FindRequestedCommand(String commandName)
        {
            return availableCommands
                .FirstOrDefault(cmd => cmd.CommandName == commandName);
        }
    }
}
