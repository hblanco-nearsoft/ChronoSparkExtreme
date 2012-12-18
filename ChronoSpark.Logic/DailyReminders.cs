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
        public void RemindStartOfDay()
        {
            var dateToday = DateTime.Now;
            if (dateToday.ToString("h tt") == "9 AM") //should get the hour from a reminder!
            {
                //do something
            }
        }

        public void RemindEndOfDay() 
        {
            var dateToday = DateTime.Now;
            if (dateToday.ToString("h tt") == "4 PM") //should get the hour from a reminder!
            {
                //do something
            }
        }
    }
}
