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
using System.Text.RegularExpressions;

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
            this.HasOption("c|Client:", "The Client for the Task at work", c => Client = c);
            this.HasOption("h|Hour:", "Hour at which the reminder will activate in format: hh:mm using 24 hours", h => HourOfActivation = h);

        }
        public String Client;
        public String Duration;
        public String Description;
        public String EntityType;
        public String IdToUpdate;
        public String HourOfActivation;

        public override int Run(string[] remainingArguments)
        {
            if(EntityType.ToLower() == "task") 
            {
                int duration;
                SparkTask  taskToFetch= new SparkTask();
                var actualId = "SparkTasks/" + IdToUpdate;
                taskToFetch.Id = actualId;
                var taskToUpdate = SparkLogic.fetch(taskToFetch) as SparkTask;

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
                Reminder reminderToFetch = new Reminder();
                var actualId = "Reminders/" + IdToUpdate;
                reminderToFetch.Id = actualId;
                var reminderToUpdate = SparkLogic.fetch(reminderToFetch) as Reminder;
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

                String pattern = @"((?<hour>\d{2})\:(?<minutes>\d{2}))";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var match = regex.Match(HourOfActivation);
                int hour, minutes;

                if (!match.Success)
                {
                    Console.WriteLine("The hour format should be hh:mm unsing 24 hours format.");
                    return 0;
                }

                if (int.TryParse(match.Groups["hour"].Value, out hour)) 
                {
                    if (hour < 00 || hour > 23) 
                    { 
                        Console.WriteLine("The hours must be between 00 and 23.");
                        return 0;
                    }
                }
                if (int.TryParse(match.Groups["minutes"].Value, out minutes)) 
                {
                    if (minutes < 00 || minutes > 59)
                    {
                        Console.WriteLine("minutes must be between 00 and 59.");
                        return 0;
                    }
                }

                DateTime ActivationTime = DateTime.Now;
                TimeSpan ts = new TimeSpan(hour, minutes, 0);
                ActivationTime = ActivationTime.Date + ts;

                reminderToUpdate.TimeOfActivation = ActivationTime;

                UpdateItemCmd updateItemCmd = new UpdateItemCmd();
                updateItemCmd.ItemToWork = reminderToUpdate;

                var result = updateItemCmd.UpdateItem();
                Console.WriteLine(result);
                return 0;
            }
            else { Console.WriteLine("the entity should be a task or reminder."); }
            return 0;
        }
    }
}
