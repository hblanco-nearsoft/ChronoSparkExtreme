using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChronoSpark.Logic
{
    public delegate void ReminderHandler(object obj, ReminderEventArgs remArgs);

    public class ReminderEventArgs : EventArgs
    {

            public readonly int TheInterval;

            public ReminderEventArgs(int interval)
            {
                TheInterval = interval;
            }

    }
}
