using System;
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
        public void RemindEndOfWeek() 
        {
            var dateToday = DateTime.Now;
            TimeSpan accumulatedTime = new TimeSpan(0,0,0);

            if(dateToday.ToString("ddd") == "Fri" && dateToday.ToString("h tt") == "4 PM") //should get the hour from a reminder!
            {
                IRepository repo = new Repository();
                var startOfWeekTime = dateToday.AddDays(-4);

                var list = repo.GetByStartDate(startOfWeekTime, dateToday);

                foreach(SparkTask task in list)
                {
                    accumulatedTime = accumulatedTime.Add(task.TimeElapsed);
                }

                if (accumulatedTime >= new TimeSpan(36, 0, 0)) 
                {
                    //call report prompt.
                }
            }
        }

        public void RemindStartOfWeek() 
        {
            var dateToday = DateTime.Now;
            TimeSpan accumulatedTime = new TimeSpan(0, 0, 0);
            if (dateToday.ToString("ddd") == "Mon" && dateToday.ToString("h tt") == "4 PM") //should get the hour from a reminder!
            {
                IRepository repo = new Repository();
                var startOfWeekTime = dateToday.AddDays(-7);
                //var dateTomorrow = dateToday.AddDays(1);

                var list = repo.GetByStartDate(startOfWeekTime, dateToday);

                foreach (SparkTask task in list)
                {
                    if(task.State != TaskState.reported)
                    {
                    accumulatedTime = accumulatedTime.Add(task.TimeElapsed);
                    }
                }

                if (accumulatedTime >= new TimeSpan(36, 0, 0))
                {
                    Console.WriteLine("say nanananana");
                    ReminderEventArgs args = new ReminderEventArgs(new Reminder(), new SparkTask()); //should be the reminder im using send the task can stay like this.
                    reminderControl.OnEventHaveToReport(args);
                }
            }
        }
    }
}
