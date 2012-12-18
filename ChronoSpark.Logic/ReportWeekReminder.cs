﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class ReportWeekReminder
    {
        ReminderControl reminderControl = new ReminderControl();
        public void RemindEndOfWeek(Reminder endOfWeekReminder) 
        {
            var dateToday = DateTime.Now;
            TimeSpan accumulatedTime = new TimeSpan(0,0,0);
            
            if(dateToday.ToString("ddd") == "Tue" && dateToday.Hour == endOfWeekReminder.TimeOfActivation.Hour ) //should get the hour from a reminder!
            {
                IRepository repo = new Repository();
                var startOfWeekTime = dateToday.AddDays(-4);

                var list = repo.GetByStartDate(startOfWeekTime, dateToday);

                foreach(SparkTask task in list)
                {
                    if (task.State != TaskState.reported){ accumulatedTime = accumulatedTime.Add(task.TimeElapsed); }
                }

                if (accumulatedTime >= new TimeSpan(36, 0, 0)) 
                {
                    ReminderEventArgs args = new ReminderEventArgs(endOfWeekReminder, new SparkTask()); //should be the reminder im using send the task can stay like this.
                    reminderControl.OnEventHaveToReport(args);
                    //event!
                }
            }
        }

        public void RemindStartOfWeek(Reminder startOfWeekReminder) 
        {
            var dateToday = DateTime.Now;
            TimeSpan accumulatedTime = new TimeSpan(0, 0, 0);
            if (dateToday.ToString("ddd") == "Mon" && dateToday.Hour == startOfWeekReminder.TimeOfActivation.Hour ) //should get the hour from a reminder!dateToday.ToString("h tt") == "4 PM"
            {
                IRepository repo = new Repository();
                var startOfWeekTime = dateToday.AddDays(-7);
                //var dateTomorrow = dateToday.AddDays(1);

                var list = repo.GetByStartDate(startOfWeekTime, dateToday);

                foreach (SparkTask task in list)
                {
                    if(task.State != TaskState.reported){ accumulatedTime = accumulatedTime.Add(task.TimeElapsed); }
                }

                if (accumulatedTime >= new TimeSpan(36, 0, 0))
                {
                    ReminderEventArgs args = new ReminderEventArgs(startOfWeekReminder, new SparkTask()); //should be the reminder im using send the task can stay like this.
                    reminderControl.OnEventHaveToReport(args);
                }
            }
        }
    }
}
