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
            this.IsCommand("reminder", "sets the reminder to activate");
            this.HasRequiredOption("r|ReminderId","The id of the reminder to activate", i => ReminderId = i);
        }

        String ReminderId;

        public override int Run(String[] RemainingArguments)
        {
            Reminder reminderTofetch = new Reminder();
            var actualId = "Reminders/" + ReminderId;
            reminderTofetch.Id = actualId;

            var reminderToSet= SparkLogic.fetch(reminderTofetch);

            //ThreadPool.QueueUserWorkItem(delegate { ReminderControl.ActivateReminder(reminderToSet); });

            return 0;
        }
    }
}