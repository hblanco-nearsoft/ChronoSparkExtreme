using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    class ReminderWorker : IWorker
    {
        public IRavenEntity getItem()
        {
            Console.WriteLine("Enter a Description for the Reminder");
            String newDescription = Console.ReadLine();
            Console.WriteLine("Enter number in minutes for the remainder interval");
            int newInterval = int.Parse(Console.ReadLine());//these should be depending on the command
            IRavenEntity workingEntity = new Reminder
            {
                Description = newDescription,
                Interval = newInterval
            };
            return workingEntity;

        }
    }
}
