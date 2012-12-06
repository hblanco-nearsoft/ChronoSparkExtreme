using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;

namespace ChronoSpark.Clients.Cli
{
    public class IntervalPassedListener
    {

        public bool Suscribe(ReminderControl reminderControl)
        {
            reminderControl.EventIntervalPassed += new ReminderControl.ReminderHandler(NotifyIntervalPassed);
            return true;
        }

        public void NotifyIntervalPassed(object obj, ReminderEventArgs args)
        {
            Console.WriteLine("the interval has passed");
        }
    }
}
