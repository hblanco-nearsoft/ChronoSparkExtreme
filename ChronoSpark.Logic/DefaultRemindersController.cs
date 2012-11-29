using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class DefaultRemindersController
    {

        public bool ActivateDefaultReminders() 
        {
            Repository repo = new Repository();
            var activeTask = repo.GetActiveTask();


            if (activeTask.Count() == 0)
            {
                IRavenEntity reminderToFetch = new Reminder();
                var actualReminderId = "Reminders/1"; 
                reminderToFetch.Id = actualReminderId;
                Reminder reminderToSet = SparkLogic.fetch(reminderToFetch) as Reminder;

                ReminderControl reminderControl = new ReminderControl();
                ThreadPool.QueueUserWorkItem(delegate { reminderControl.ActivateReminder(reminderToSet, new SparkTask()); });
            }
            if (activeTask.Count() == 1) 
            {
                IRavenEntity reminderToFetch = new Reminder();
                var actualReminderId = "Reminders/2";
                reminderToFetch.Id = actualReminderId;
                Reminder reminderToSet = SparkLogic.fetch(reminderToFetch) as Reminder;

                ReminderControl reminderControl = new ReminderControl();
                ThreadPool.QueueUserWorkItem(delegate { reminderControl.ActivateReminder(reminderToSet, new SparkTask()); });
         
            }

            return true;
        }

    }
}
