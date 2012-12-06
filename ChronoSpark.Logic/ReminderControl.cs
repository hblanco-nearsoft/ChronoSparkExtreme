using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using System.Threading;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class ReminderControl
    {
        public delegate void ReminderHandler(object obj, ReminderEventArgs remArgs);

        public static ReminderHandler _eventNoActiveTask;
        public static ReminderHandler _eventIntervalPassed;

        public event ReminderHandler EventNoActiveTask
        {
            add 
            {
                if (_eventNoActiveTask == null || !_eventNoActiveTask.GetInvocationList().Contains(value)) 
                {
                    _eventNoActiveTask += value;
                }
            }
            
            remove { _eventNoActiveTask -= value; }
        }

        public event ReminderHandler EventIntervalPassed 
        {
            add 
            {
                if (_eventIntervalPassed == null || !_eventIntervalPassed.GetInvocationList().Contains(value)) 
                {
                    _eventIntervalPassed += value;
                }
            }

            remove { _eventIntervalPassed -= value; }
        }

        public void ActivateReminders(IEnumerable<Reminder> receivedListOfReminders)
        {
            //ReminderListener listener = new ReminderListener();
            //EventReminder += new ReminderHandler(listener.ActivateReminder);
            GetReminded(receivedListOfReminders);
        }

        public  void OnEventNoActiveTask(ReminderEventArgs args) 
        {
            if (_eventNoActiveTask != null) { _eventNoActiveTask(this, args); }
        }

        public void OnEventIntervalPassed(ReminderEventArgs args) 
        {
            if (_eventIntervalPassed != null) { _eventIntervalPassed(this, args); }
        }

        public static void GetReminded(IEnumerable<Reminder> listOfReminders) 
        {
           // int minutes = 0;
            var starTime = DateTime.Now;
           IRepository repo = new Repository();
 
            while (true)
            {
              //  minutes++;
                var timeElapsed = DateTime.Now - starTime;
                var activeTasks = repo.GetActiveTask();

                foreach (Reminder r in listOfReminders)
                {
                    ReminderEventArgs eventArgs = new ReminderEventArgs(r);
                    ReminderControl reminderControl = new ReminderControl();
                    if (timeElapsed.Minutes % r.Interval == 0 && activeTasks.Count() == 0 && r.Id == "reminders/1")
                    { 
                        reminderControl.OnEventNoActiveTask(eventArgs);
                    }
                    if (timeElapsed.Minutes % r.Interval == 0 && activeTasks.Count() > 0 && r.Id == "reminders/2") 
                    {
                        reminderControl.OnEventIntervalPassed(eventArgs);    
                    }

                }
            }
        }
    }
}