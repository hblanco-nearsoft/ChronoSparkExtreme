using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;

namespace ChronoSpark.Clients.Cli
{
    class UpdateCommand : ConsoleCommand
    {
        public UpdateCommand() 
        {
            this.IsCommand("Update", "Updates the info on the selected item");
            this.HasRequiredOption("e|EntityType=", "The type of entity you want to add (task or reminder)", e => EntityType = e);
            this.HasRequiredOption("i|id=", "The Id of the item to change", i => IdToUpdate = i);

            this.HasOption("d|Description:", "A description for the item to create", d => Description = d);
            this.HasOption("t|Time", "The Duration of the task or remainder interval", t => Duration = t);
            this.HasOption("c|Client", "The Client for the Task at work", c => Client = c);


        }
        public String Client;
        public String Duration;
        public String Description;
        public String EntityType;
        public String IdToUpdate;

        public override int Run(string[] remainingArguments)
        {
            if(EntityType.ToLower() == "task") 
            {
                int duration;
                SparkTask  taskToUpdate= new SparkTask();
                var actualId = "SparkTasks/" + IdToUpdate;
                taskToUpdate.Id = actualId;
                taskToUpdate.Id = IdToUpdate;
                taskToUpdate.Description = Description;
                if (int.TryParse(Duration, out duration))
                {
                taskToUpdate.Duration = duration;
                }
                taskToUpdate.Client = Client;

                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("update");
                theCommand.ItemToWork = taskToUpdate;
                var result = SparkLogic.ProcessCommand(theCommand);
                Console.WriteLine(result);
                return 0;
            }
            if (EntityType.ToLower() == "reminder") 
            {
            }
            else { Console.WriteLine("the entity should be a task or reminder"); }
            return 0;
        }
    }
}
