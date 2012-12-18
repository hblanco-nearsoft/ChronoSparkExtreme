using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;

namespace ChronoSpark.Clients.Cli
{
    public class TimeToReportListener
    {
        public bool Suscribe(ReminderControl reminderControl)
        {
            reminderControl.EventHaveToReportWeek += new ReminderControl.ReminderHandler(NotifyNeedToReport);
            return true;
        }

        public void NotifyNeedToReport(object obj, ReminderEventArgs args)
        {
            Console.WriteLine("You have reached 36 worked hours! Would you like to send a report now?");
        }
    }
}
