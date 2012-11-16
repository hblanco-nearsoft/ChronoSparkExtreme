using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Logic;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    public class AddCommand : ConsoleCommand
    {
        public AddCommand() 
        {
            this.IsCommand("Add", "Adds a new task or reminder to the database");
            this.HasRequiredOption("e|EntityType=", "The type of entity you want to add (task or reminder)", e => EntityType = e);
            this.HasRequiredOption("d|Description=", "A description for the item to create", d => Description = d);
            this.HasRequiredOption("t|Time=", "Duration for a task or the interval of a reminder (in minutes)", t => Duration = t);
            this.HasOption("c|Client=", "The name of the client for the task", c => Client = c);
            
        }

        public String EntityType;
        public String Description;
        public String Duration;
        public String Client;

        public override int Run(string[] remainingArguments)
        {
            
            if(EntityType.ToLower() == "task")
            {
                SparkTask taskToAdd = new SparkTask();
                taskToAdd.Description = Description;
                int duration;
                if (int.TryParse(Duration, out duration))
                {
                    if (duration <= 0) 
                    {
                        Console.WriteLine("The duration must be a number grather than 0");
                        return 0;
                    }
                    taskToAdd.Duration = duration;
                }
                else
                {
                    Console.WriteLine("The duration must be an integer");
                    return 0;
                }
                taskToAdd.Client = Client;
                taskToAdd.StartDate = DateTime.Now;
                taskToAdd.State = TaskState.Paused;

                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("add");
                theCommand.ItemToWork = taskToAdd;
                var result =SparkLogic.ProcessCommand(theCommand);
                Console.WriteLine(result);
                return 0;
                
            }

            if (EntityType.ToLower() == "reminder")
            {
                Reminder reminderToAdd = new Reminder();
                reminderToAdd.Description = Description;
                int interval;
                if (int.TryParse(Duration, out interval))
                {
                    if (interval <= 0)
                    {
                        Console.WriteLine("The duration must be a number grather than 0");
                        return 0;
                    }
                    reminderToAdd.Interval = interval;
                }
                else
                {
                    Console.WriteLine("The duration must be an integer");
                    return 0;
                }
                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("add");
                theCommand.ItemToWork = reminderToAdd;
                var result = SparkLogic.ProcessCommand(theCommand);
                Console.WriteLine(result);
                return 0;
            }
            else { Console.WriteLine("The type of entity should be a task or reminder"); }

            return 0;
        }
    }
}
