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

            SparkTask theOtherTask = new SparkTask
            {
                Description = "task number 2",
                Duration = 5,
                Client = "Client number 2",
                StartDate = DateTime.Now,
                State = TaskState.Paused,
                TimeElapsed = new TimeSpan(36, 0, 0)
            };


            Reminder thisReminder = new Reminder
            {
                Description = "test reminder",
                Interval = 10,
                Type = ReminderType.NoActiveTask,
                Source = ReminderSource.System
            };


            Reminder thisReminder2 = new Reminder
            {
                Description = "reminder 2",
                Interval = 1,
                Type = ReminderType.DefaultHourly,
                Source = ReminderSource.System
            };
            

            //ThreadPool.QueueUserWorkItem(delegate { ReminderControl.ActivateReminder(thisReminder, thisTask); });
#endif
            #endregion  
            

            DateTime EntranceTime = DateTime.Now;
            TimeSpan ts = new TimeSpan(9, 0, 0);
            EntranceTime = EntranceTime.Date + ts;

            DateTime ExitTime = DateTime.Now;
            TimeSpan ts2 = new TimeSpan(15, 0, 0);
            ExitTime = ExitTime.Date + ts2;

            Repository repo = new Repository();
            Reminder DefaultReminderNoOtherActive = new Reminder
            {
                Description = "There is no active task.",
                Interval = 120,
                Type = ReminderType.NoActiveTask,
                Source= ReminderSource.System
            };
            Reminder DefaultHourlyReminder = new Reminder
            {
                Description = "An hour has passed in the task.",
                Interval = 60,
                Type = ReminderType.DefaultHourly,
                Source= ReminderSource.System
            };

            Reminder StartOfWeek = new Reminder
            {
                Description = "Start of the week.",
                Interval = 7,
                TimeOfActivation = EntranceTime,
                Type = ReminderType.StartOfWeek,
                Source= ReminderSource.System
            };
            Reminder EndOfWeek = new Reminder
            {
                Description = "The end of week.",
                Interval = 7,
                TimeOfActivation = ExitTime,
                Type = ReminderType.EndOfWeek,
                Source= ReminderSource.System
            };
            Reminder StartOfDay = new Reminder
            {
                Description = "Start of The day",
                Interval = 8,
                Type = ReminderType.StartOfDay,
                Source= ReminderSource.System
            };
            Reminder EndOfDay = new Reminder
            {
                Description = "End of the Day",
                Interval = 8,
                Type = ReminderType.EndOfDay,
                Source= ReminderSource.System
            };

            #region debug
#if DEBUG
            repo.Add(thisTask);
            repo.Add(thisReminder);
            repo.Add(thisReminder2);
            repo.Add(theOtherTask);
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
