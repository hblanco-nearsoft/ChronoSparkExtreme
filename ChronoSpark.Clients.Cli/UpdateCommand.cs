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
            this.HasOption("t|Time:", "Duration for a task or the interval of a reminder", t => Duration = t);
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
                taskToUpdate.Description = Description;
                if (int.TryParse(Duration, out duration))
                {
                    if (duration <= 0)
                    {
                        Console.WriteLine("The duration must be greater than 0");
                        return 0;
                    }
                    taskToUpdate.Duration = duration;
                }

                UpdateItemCmd updateItemCmd = new UpdateItemCmd();
                updateItemCmd.ItemToWork = taskToUpdate;

                var result = updateItemCmd.UpdateItem();
                Console.WriteLine(result);
                return 0;
            }
            if (EntityType.ToLower() == "reminder") 
            {
                int interval;
                Reminder reminderToUpdate = new Reminder();
                var actualId = "Reminders/" + IdToUpdate;
                reminderToUpdate.Id = actualId;
                reminderToUpdate.Description = Description;
                //if (!int.TryParse(Duration, out interval)) 
                //{
                //    Console.WriteLine("The duration of the interval must be an integer");
                //    return 0;
                //}
                if (int.TryParse(Duration, out interval))
                {
                    if (interval <= 0) 
                    {
                        Console.WriteLine("The interval must be greater than 0");
                        return 0;
                    }
                    reminderToUpdate.Interval = interval;
                }
                UpdateItemCmd updateItemCmd = new UpdateItemCmd();
                updateItemCmd.ItemToWork = reminderToUpdate;

                var result = updateItemCmd.UpdateItem();
                Console.WriteLine(result);
                return 0;
            }
            else { Console.WriteLine("the entity should be a task or reminder"); }
            return 0;
        }
    }
}
