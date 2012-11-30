using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public class ReminderListener
    {
        public void ActivateReminder(object obj, ReminderEventArgs args)
        {
            Console.WriteLine("{0} minutes have passed in the Reminder: {1}", args.TheReminder.Interval, args.TheReminder.Description);
        }
    }
}
