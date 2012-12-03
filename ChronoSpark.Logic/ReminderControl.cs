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

        public void ActivateReminders(IEnumerable<Reminder> receivedListOfReminders)
        {
            ReminderListener listener = new ReminderListener();
            EventReminder += new ReminderHandler(listener.ActivateReminder);
            GetReminded(receivedListOfReminders);
        }

        public static void OnEventReminder(ReminderEventArgs args) 
        {
            if (_EventReminder != null) { _EventReminder(new object(), args); }
        }

        public static void GetReminded(IEnumerable<Reminder> listOfReminders) 
        {
           // int minutes = 0;
            var starTime = DateTime.Now;

            while (true)
            {
                Thread.Sleep(60005);
              //  minutes++;
                var timeElapsed = DateTime.Now - starTime;

                foreach (Reminder r in listOfReminders)
                {

                    if (timeElapsed.Minutes % r.Interval == 0)
                    {
                        ReminderEventArgs eventArgs = new ReminderEventArgs(r);
                        OnEventReminder(eventArgs);
                    }
                }
                //var intervalToWait = theReminder.Interval * 1000;
                //Thread.Sleep(intervalToWait);
                //ReminderEventArgs eventArgs = new ReminderEventArgs(theReminder, theTask);
                //OnEventReminder(eventArgs);

            }
        }
    }
}