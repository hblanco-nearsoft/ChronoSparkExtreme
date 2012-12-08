using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    public class NoActiveTaskListener
    {

        public bool Suscribe(ReminderControl reminderControl) 
        {
            reminderControl.EventNoActiveTask += new ReminderControl.ReminderHandler(NotifyLackOfActiveTask);
            return true;
        }

        public void NotifyLackOfActiveTask(object obj, ReminderEventArgs args) 
        {
            Console.WriteLine("There is no Active Task.");
        }
    }
}
