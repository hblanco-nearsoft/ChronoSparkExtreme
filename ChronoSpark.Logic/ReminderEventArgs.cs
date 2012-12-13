using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{

    public class ReminderEventArgs : EventArgs
    {

        public readonly Reminder TheReminder;
        public readonly SparkTask TheTask;

        public ReminderEventArgs(Reminder receivedReminder, SparkTask receivedTask)
        {
            TheReminder = receivedReminder;
            TheTask = receivedTask;
        }
    }
}
