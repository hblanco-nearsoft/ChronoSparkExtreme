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


        public static Boolean ShouldResetStartTime = false;
        public void ActivateReminders(IEnumerable<Reminder> listOfReminders) 
        {   
           // int minutes = 0;
            
            IRepository repo = new Repository();
            var startTime = DateTime.Now;
            ActiveTaskProcess taskProcessor = new ActiveTaskProcess();

            while (true)
            {
                 if (ShouldResetStartTime)
                {
                    startTime = DateTime.Now;
                    ShouldResetStartTime = false;
                }
                Thread.Sleep(60005);

                var timeElapsed = DateTime.Now - startTime;
                var activeTask = repo.GetActiveTask();

                if (activeTask != null)
                {
                    taskProcessor.AddElapsedTime();
                }

                foreach (Reminder r in listOfReminders)
                {

                    ReminderEventArgs eventArgs = new ReminderEventArgs(r);
                    ReminderControl reminderControl = new ReminderControl();
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