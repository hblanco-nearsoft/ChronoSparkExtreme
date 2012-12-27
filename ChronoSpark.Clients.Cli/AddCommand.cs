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
using System.Text.RegularExpressions;

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
            this.HasOption("h|Hour=", "The hour at wich the reminder would activate", h => TimeOfActivation = h);
        }

        public String EntityType;
        public String Description;
        public String Duration;
        public String Client;
        public String TimeOfActivation;

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


                AddItemCmd addItemCmd = new AddItemCmd();
                
                addItemCmd.ItemToWork = taskToAdd;
                
                var result = addItemCmd.AddItem();
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



                String pattern = @"((?<hour>\d{2})\:(?<minutes>\d{2}))";
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var match = regex.Match(TimeOfActivation);
                int hour, minutes;

                if (!int.TryParse(match.Groups["hour"].Value, out hour) || !int.TryParse(match.Groups["minutes"].Value, out minutes))
                {
                    Console.WriteLine("The hour has to be numbers");
                    return 0;
                }

                if (int.TryParse(match.Groups["hour"].Value, out hour))
                {
                    if (hour < 00 && hour > 23)
                    {
                        Console.WriteLine("The hours must be between 00 and 23");
                        return 0;
                    }
                }
                if (int.TryParse(match.Groups["minutes"].Value, out minutes))
                {
                    if (minutes < 00 && minutes > 59)
                    {
                        Console.WriteLine("minutes must be between 00 and 59");
                        return 0;
                    }
                }

                DateTime ActivationTime = DateTime.Now;
                TimeSpan ts = new TimeSpan(hour, minutes, 0);
                ActivationTime = ActivationTime.Date + ts;

                reminderToAdd.TimeOfActivation = ActivationTime;
                reminderToAdd.Type = ReminderType.Custom;
                reminderToAdd.Source = ReminderSource.User;

                AddItemCmd addItemCmd = new AddItemCmd();

                addItemCmd.ItemToWork = reminderToAdd;


                var result = addItemCmd.AddItem();
                Console.WriteLine(result);
                return 0;
            }
            else { Console.WriteLine("The type of entity should be a task or reminder"); }

            return 0;
        }
    }
}
