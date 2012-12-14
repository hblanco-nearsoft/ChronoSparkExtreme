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
            var actualReminderId = "Reminders/" + ReminderId;
            reminderToFetch.Id = actualReminderId;

            Reminder reminderToSet= SparkLogic.fetch(reminderToFetch) as Reminder;
           
            if (reminderToSet != null)
            {
                ReminderControl reminderControl = new ReminderControl();
            }
            return 0;
        }
    }
}