using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class ReminderListener
    {
        public void ActivateReminder(object obj, ReminderEventArgs args)
        {

            IRepository repo = new Repository();
            var activeTask = repo.GetActiveTask();

            if (args.TheReminder.Id == "reminders/1" && activeTask.Count() == 0)
            {
                Console.WriteLine("There's currently no active task. Would you like to start one?");
                
            }
            if (args.TheReminder.Id == "reminders/2" && activeTask.Count() > 0)
            {
                Console.WriteLine("{0} minutes have passed in the Reminder: {1}", args.TheReminder.Interval, args.TheReminder.Description);
            }


        }
    }
}