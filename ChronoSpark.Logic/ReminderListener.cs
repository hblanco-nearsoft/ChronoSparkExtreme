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
            Console.WriteLine("IT IS TIME!");
        }
    }
}
