using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class DailyReminders
    {
        public void RemindStartOfDay(Reminder startOfDayReminder)
        {
            var dateToday = DateTime.Now;
            if (dateToday.Hour == startOfDayReminder.TimeOfActivation.Hour && dateToday.Minute == startOfDayReminder.TimeOfActivation.Minute) //should get the hour from a reminder!
            {
                //do something
            }
        }

        public void RemindEndOfDay(Reminder endOfDayReminder) 
        {
            var dateToday = DateTime.Now;
            if (dateToday.Hour == endOfDayReminder.TimeOfActivation.Hour && dateToday.Minute == endOfDayReminder.TimeOfActivation.Minute) //should get the hour from a reminder!
            {
                //do something
            }
        }
    }
}
