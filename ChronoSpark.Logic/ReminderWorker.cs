using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    class ReminderWorker
    {
        public IRavenEntity getReminder(String newDescription, int newInterval)
        {

            IRavenEntity workingEntity = new Reminder
            {
                Description = newDescription,
                Interval = newInterval
            };
            return workingEntity;

        }
    }
}
