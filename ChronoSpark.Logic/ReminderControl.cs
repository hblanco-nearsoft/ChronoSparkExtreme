using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using System.Threading;

namespace ChronoSpark.Logic
{
    public class ReminderControl
    {
        public static event ReminderHandler EventReminder;

        public static void ActivateReminder(Reminder theReminder, SparkTask TheTask)
        {
            ReminderListener listener = new ReminderListener();
            EventReminder += new ReminderHandler(listener.ActivateReminder);
            GetReminded(theReminder, TheTask);
        }

        public static void OnEventReminder(ReminderEventArgs args) 
        {
            if (EventReminder != null) { EventReminder(new object(), args); }
        }

        public static void GetReminded(Reminder theReminder, SparkTask theTask) 
        {
            while (true)
            {
                var nowTime = DateTime.Now;
                //var sleepInterval = interval * 60 * 1000;
                var theInterval = theReminder.Interval;
                var theTime = nowTime.AddMinutes(theInterval);
                var reminded = false;
                while (!reminded)
                { 
                    if (DateTime.Compare(theTime, DateTime.Now) == 0)
                    {
                        ReminderEventArgs i = new ReminderEventArgs(theReminder,theTask);
                        //Thread.Sleep(sleepInterval);
                        OnEventReminder(i);
                        reminded = true;
                    }
                    if (DateTime.Compare(theTime, DateTime.Now) > 0) { reminded = true; }
                }
            }
        }
    }
}
