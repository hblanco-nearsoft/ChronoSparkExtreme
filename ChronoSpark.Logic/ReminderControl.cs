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
        public delegate void ReminderHandler(object obj, ReminderEventArgs reminderArgs);

        public static ReminderHandler _eventNoActiveTask;
        public static ReminderHandler _eventIntervalPassed;
        public static ReminderHandler _eventHaveToReportWeek;
        public static ReminderHandler _eventItIsTimeOfActivation;
       
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

        public event ReminderHandler EventHaveToReportWeek 
        {
            add
            {
                if (_eventHaveToReportWeek == null || !_eventHaveToReportWeek.GetInvocationList().Contains(value))
                {
                    _eventHaveToReportWeek += value;
                }
            }
            remove { _eventHaveToReportWeek -= value; }
        }

        public event ReminderHandler EventItIsTimeOfActivation
        {
            add 
            {
                if (_eventItIsTimeOfActivation == null || !_eventItIsTimeOfActivation.GetInvocationList().Contains(value)) 
                {
                    _eventItIsTimeOfActivation += value;
                }
            }
            remove { _eventItIsTimeOfActivation -= value; }
        }

        public void OnEventHaveToReport(ReminderEventArgs args) 
        {
            if (_eventHaveToReportWeek != null) { _eventHaveToReportWeek(this, args); }
        }
                
        public  void OnEventNoActiveTask(ReminderEventArgs args) 
        {
            if (_eventNoActiveTask != null) { _eventNoActiveTask(this, args); }
        }

        public void OnEventIntervalPassed(ReminderEventArgs args) 
        {
            if (_eventIntervalPassed != null) { _eventIntervalPassed(this, args); }
        }

        public void OnEventItIsTimeOfActivation(ReminderEventArgs args)
        {
            if (_eventItIsTimeOfActivation != null) { _eventItIsTimeOfActivation(this, args); }
        }

        public static DateTime StartTime;
        public void ActivateReminders() 
        {   
           // int minutes = 0;
            
            IRepository repo = new Repository();
            StartTime = DateTime.Now;
            ActiveTaskProcess taskProcessor = new ActiveTaskProcess();
            ReportWeekReminder reportWeekReminder = new ReportWeekReminder();
            DailyReminders dailyReminders = new DailyReminders();

            while (true)
            {
                
                Thread.Sleep(60000);
                var listOfReminders = SparkLogic.ReturnReminderList();

                var timeElapsed = DateTime.Now - StartTime;
                var activeTask = repo.GetActiveTask();

                if (activeTask != null)
                {
                    taskProcessor.AddElapsedTime();
                }

                foreach (Reminder reminder in listOfReminders)
                {
                    
                    ReminderEventArgs eventArgs = new ReminderEventArgs(reminder, activeTask);
                    ReminderControl reminderControl = new ReminderControl();


                    if (timeElapsed.Minutes >= 1)
                    {
                        if (timeElapsed.Minutes % reminder.Interval == 0 && activeTask == null && reminder.Type == ReminderType.NoActiveTask)
                        {
                            reminderControl.OnEventNoActiveTask(eventArgs);
                        }
                        if (timeElapsed.Minutes % reminder.Interval == 0 && activeTask != null && reminder.Type == ReminderType.DefaultHourly)
                        {
                            reminderControl.OnEventIntervalPassed(eventArgs);
                        }                        
                        if (reminder.Type==ReminderType.EndOfWeek) { reportWeekReminder.RemindEndOfWeek(reminder); }
                        if (reminder.Type==ReminderType.StartOfWeek) { reportWeekReminder.RemindStartOfWeek(reminder); }
                        if (reminder.Type == ReminderType.StartOfDay) { dailyReminders.RemindStartOfDay(reminder); }
                        if (reminder.Type == ReminderType.EndOfDay) { dailyReminders.RemindEndOfDay(reminder); }
                    }
                }
            }
        }
    }
}