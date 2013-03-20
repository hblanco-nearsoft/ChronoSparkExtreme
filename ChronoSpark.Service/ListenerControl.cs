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
            EventModel model = new EventModel();
            model.Type = EventType.IntervalPassed;
            model.SourceTask = args.TheTask;
            EventController.RegisterEvent(model);
        }

        public void NotifyNeedToReport(object obj, ReminderEventArgs args)
        {
            EventModel model = new EventModel();
            model.Type = EventType.EndOfWeek;
            EventController.RegisterEvent(model);
        }

        public void NotifyLackOfActiveTask(object obj, ReminderEventArgs args)
        {
            EventModel model = new EventModel();
            model.Type = EventType.NoActiveTask;
            EventController.RegisterEvent(model);
        }
    }
}
