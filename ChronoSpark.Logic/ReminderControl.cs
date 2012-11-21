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

        public static void ActivateReminder(Reminder theReminder)
        {
            ReminderListener listener = new ReminderListener();
            EventReminder += new ReminderHandler(listener.ActivateReminder);
            GetReminded(theReminder.Interval);
        }

        public static void OnEventReminder(ReminderEventArgs args) 
        {
            if (EventReminder != null) { EventReminder(new object(), args); }
        }

        public static void GetReminded(int interval) 
        {
            while (true)
            {
                var theTime = DateTime.Now;
                theTime.AddMinutes(interval);
                var reminded = false;
                while (!reminded)
                {
                    if (theTime == DateTime.Now)
                    {
                        ReminderEventArgs i = new ReminderEventArgs(1);
                        Thread.Sleep(5000);
                        OnEventReminder(i);
                        reminded = true;
                    }
                }
            }
        }
    }
}
