using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Logic;

namespace ChronoSpark.Service
{
    public class ListenerControl
    {
        public bool Suscribe(ReminderControl reminderControl)
        {
            reminderControl.EventIntervalPassed += new ReminderControl.ReminderHandler(NotifyIntervalPassed);
            reminderControl.EventNoActiveTask += new ReminderControl.ReminderHandler(NotifyLackOfActiveTask);
            reminderControl.EventHaveToReportWeek += new ReminderControl.ReminderHandler(NotifyNeedToReport);

            return true;
        }

        public void NotifyIntervalPassed(object obj, ReminderEventArgs args)
        {
            EventModel model = new EventModel
            {
                Type = EventType.IntervalPassed,
                SourceTask = args.TheTask,
                Name = "Interval Passed",
                Message = "Are you still working on '"+ args.TheTask.Description  +"'?"
            };
            HomeController.RegisterEvent(model);
        }

        public void NotifyNeedToReport(object obj, ReminderEventArgs args)
        {
            EventModel model = new EventModel 
            { 
                Type = EventType.EndOfWeek,
                Name = "End Of Week",
                Message = "You completed 36 hours, you should report"
            };

            HomeController.RegisterEvent(model);
        }

        public void NotifyLackOfActiveTask(object obj, ReminderEventArgs args)
        {
            EventModel model = new EventModel 
            {
                Type = EventType.NoActiveTask,
                Name = "No active Task",
                Message = "There is no active task, would you like to start working on one?"
            
            };
            HomeController.RegisterEvent(model);
        }
    }
}
