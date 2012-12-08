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
using System.Threading;

namespace ChronoSpark.Clients.Cli
{
    public class SetReminderCommand : ConsoleCommand
    {
        public SetReminderCommand() 
        {
            this.IsCommand("set", "sets the reminder to activate");
            this.HasRequiredOption("r|Reminder=","The id of the reminder to activate", r => ReminderId = r);
            this.HasRequiredOption("t|Task=", "The id of the task to activate", t => TaskId = t);
        }

        public String ReminderId;
        public String TaskId;

        public override int Run(String[] RemainingArguments)
        {
            IRavenEntity reminderToFetch = new Reminder();
            IRavenEntity taskToFetch = new SparkTask();
            var actualReminderId = "Reminders/" + ReminderId;
            var actualTaskId = "SparkTasks/" + TaskId;
            reminderToFetch.Id = actualReminderId;
            taskToFetch.Id = actualTaskId;

            Reminder reminderToSet= SparkLogic.fetch(reminderToFetch) as Reminder;
           
            SparkTask taskToSet = SparkLogic.fetch(taskToFetch) as SparkTask;

            if (taskToSet != null && reminderToSet != null)
            {
                ReminderControl reminderControl = new ReminderControl();
               // ThreadPool.QueueUserWorkItem(delegate { reminderControl.ActivateReminders(reminderToSet, taskToSet); });
            }
            return 0;
        }
    }
}