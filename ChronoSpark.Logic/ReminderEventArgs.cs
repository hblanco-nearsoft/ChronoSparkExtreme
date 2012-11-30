﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public delegate void ReminderHandler(object obj, ReminderEventArgs remArgs);

    public class ReminderEventArgs : EventArgs
    {

        public readonly Reminder TheReminder;    

            public ReminderEventArgs(Reminder receivedReminder)
            {
                TheReminder = receivedReminder;
            }

    }
}
