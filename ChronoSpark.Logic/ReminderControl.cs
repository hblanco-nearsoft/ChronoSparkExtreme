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
        private static ReminderHandler _EventReminder;

        public event ReminderHandler EventReminder
        {
            add 
            {
                if (_EventReminder == null || _EventReminder.GetInvocationList().Contains(value)) 
                {
                    _EventReminder += value;
                }
            }

            remove { _EventReminder -= value; }
        }

        public void ActivateReminder(Reminder theReminder, SparkTask TheTask)
        {
            ReminderListener listener = new ReminderListener();
            EventReminder += new ReminderHandler(listener.ActivateReminder);
            GetReminded(theReminder, TheTask);
        }

        public static void OnEventReminder(ReminderEventArgs args) 
        {
            if (_EventReminder != null) { _EventReminder(new object(), args); }
        }

        public static void GetReminded(Reminder theReminder, SparkTask theTask) 
        {
            while (true)
            {

                var intervalToWait = theReminder.Interval * 1000;
                Thread.Sleep(intervalToWait);
                ReminderEventArgs eventArgs = new ReminderEventArgs(theReminder, theTask);
                OnEventReminder(eventArgs);
                //var nowTime = DateTime.Now;
                //var theInterval = theReminder.Interval;
                //var theTime = nowTime.AddMinutes(theInterval);
                //var reminded = false;
                //while (!reminded)
                //{ 
                //    if (DateTime.Compare(theTime, DateTime.Now)== 0)
                //    {
                //        ReminderEventArgs i = new ReminderEventArgs(theReminder,theTask);
                //        OnEventReminder(i);
                //        reminded = true;
                //    }
                //    if (DateTime.Compare(theTime, DateTime.Now) < 0) {reminded = true; }
                //}
            }
        }
    }
}