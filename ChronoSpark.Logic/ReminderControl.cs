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
        public static ReminderHandler _eventMinutePassed;

        public event ReminderHandler EventMinutePassed 
        {
            add 
            {
                if (_eventMinutePassed == null || !_eventMinutePassed.GetInvocationList().Contains(value)) 
                {
                    _eventMinutePassed += value;
                }
            }
            remove { _eventMinutePassed -= value; } 
        }
        
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



        public  void OnEventNoActiveTask(ReminderEventArgs args) 
        {
            if (_eventNoActiveTask != null) { _eventNoActiveTask(this, args); }
        }

        public void OnEventIntervalPassed(ReminderEventArgs args) 
        {
            if (_eventIntervalPassed != null) { _eventIntervalPassed(this, args); }
        }

        public void OnEventMinutePassed(ReminderEventArgs args)
        {
            if (_eventMinutePassed != null) { _eventMinutePassed(this, args); }
        }

        public void ActivateReminders(IEnumerable<Reminder> listOfReminders) 
        {
           // int minutes = 0;
            var starTime = DateTime.Now;
           IRepository repo = new Repository();
 
            while (true)
            {
                Thread.Sleep(60005);
              //  minutes++;
                var timeElapsed = DateTime.Now - starTime;
                var activeTasks = repo.GetActiveTask();

                foreach (Reminder r in listOfReminders)
                {
                    foreach (SparkTask activeTask in activeTasks) 
                    {
                        activeTask.TimeElapsed = activeTask.TimeElapsed.Add(timeElapsed);
                    }
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