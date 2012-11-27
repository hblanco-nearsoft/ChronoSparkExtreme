using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Data
{
    public class DefaultReminders
    {
        public static bool AddDefaultReminders() 
        {

            #region debug
#if DEBUG
            SparkTask thisTask = new SparkTask
            {
                Description = "auto Task default premium plus",
                Duration = 10,
                Client = "Client",
                StartDate = DateTime.Now,
                State = TaskState.Paused
            };
            

            Reminder thisReminder = new Reminder
            {
                Description = "reminder 1",
                Interval = 1
            };
           

            Reminder thisReminder2 = new Reminder
            {
                Description = "reminder 2",
                Interval = 1
            };
            

            //ThreadPool.QueueUserWorkItem(delegate { ReminderControl.ActivateReminder(thisReminder, thisTask); });
#endif
            #endregion  

            Repository repo = new Repository();
            Reminder DefaultReminderNoOtherActive = new Reminder
            {
                Description = "There is no active task.",
                Interval = 30
            };
            Reminder DefaultHourlyReminder = new Reminder
            {
                Description = "An hour has passed.",
                Interval = 60
            };
            Reminder StartOfWeek = new Reminder
            {
                Description = "Start of the week.",
                Interval = 7
            };
            Reminder EndOfWeek = new Reminder
            {
                Description = "The end of week.",
                Interval = 7
            };
            Reminder StartOfDay = new Reminder
            {
                Description = "Start of The day",
                Interval = 24
            };
            Reminder EndOfDay = new Reminder
            {
                Description = "End of the Day",
                Interval = 24
            };

            #region debug
#if DEBUG
            repo.Add(thisTask);
            repo.Add(thisReminder);
            repo.Add(thisReminder2);
#endif
            #endregion

            repo.Add(DefaultReminderNoOtherActive);
            repo.Add(DefaultHourlyReminder);
            repo.Add(StartOfWeek);
            repo.Add(StartOfDay);
            repo.Add(EndOfDay);
            repo.Add(EndOfWeek);

            return true;
        }        
    }
}
