using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    public class AddCommand : ConsoleCommand
    {
        public AddCommand() 
        {
            this.IsCommand("Add", "Adds a new task or reminder to the database");
            this.HasRequiredOption("EntityType=", "The type of entity you want to add (task or reminder)", s => { });
            this.HasRequiredOption("Description=", "A description for the item to create", s => { });
            this.HasOption("Client=", "The name of the client we want to register", c => { });

            HasAdditionalArguments(3, "<EntityType> <Description> <Client>");
            
        }

        public String EntityType;
        public String Description;
        public String Duration;
        public int duration;
        public String Client;

        public override int Run(string[] remainingArguments)
        {
            EntityType  = remainingArguments[0];
            Description = remainingArguments[1];
            Client = remainingArguments[2];

            if(EntityType == "task")
            {
                SparkTask taskToAdd = new SparkTask();
                taskToAdd.Description = Description;
                if (int.TryParse(Duration, out duration))
                {
                    taskToAdd.Duration = duration;
                }
                taskToAdd.Client = Client;
                taskToAdd.StartDate = DateTime.Now;
                taskToAdd.State = TaskState.Paused;
            }

            return 0;
        }
    }
}
