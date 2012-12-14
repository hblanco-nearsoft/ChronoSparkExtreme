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
                
        public  void OnEventNoActiveTask(ReminderEventArgs args) 
        {
            if (_eventNoActiveTask != null) { _eventNoActiveTask(this, args); }
        }

        public void OnEventIntervalPassed(ReminderEventArgs args) 
        {
            if (_eventIntervalPassed != null) { _eventIntervalPassed(this, args); }
        }

        public static DateTime StartTime;
        public void ActivateReminders(IEnumerable<Reminder> listOfReminders) 
        {   
           // int minutes = 0;
            
            IRepository repo = new Repository();
            StartTime = DateTime.Now;
            ActiveTaskProcess taskProcessor = new ActiveTaskProcess();

            while (true)
            {
                Thread.Sleep(60000);

                var timeElapsed = DateTime.Now - StartTime;
                var activeTask = repo.GetActiveTask();

                if (activeTask != null)
                {
                    taskProcessor.AddElapsedTime();
                }

                foreach (Reminder r in listOfReminders)
                {

                    ReminderEventArgs eventArgs = new ReminderEventArgs(r, activeTask);
                    ReminderControl reminderControl = new ReminderControl();
                    if (timeElapsed.Minutes >= 1)
                    {
                        if (timeElapsed.Minutes % r.Interval == 0 && activeTask == null && r.Id == "reminders/1")
                        {
                            reminderControl.OnEventNoActiveTask(eventArgs);
                        }
                        if (timeElapsed.Minutes % r.Interval == 0 && activeTask != null && r.Id == "reminders/2")
                        {
                            reminderControl.OnEventIntervalPassed(eventArgs);
                        }
                    }
                }
            }
        }
    }
}