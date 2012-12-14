using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class EndOfWeekReminder
    {

        public void RemindEndOfWeek() 
        {
            var date = DateTime.Now;
            TimeSpan accumulatedTime = new TimeSpan(0,0,0);

            if(date.ToString("ddd") == "Fri" && date.ToString("h tt") == "4 PM") //should get the hour from a reminder!
            {
                IRepository repo = new Repository();
                var startOfWeekTime = date.AddDays(-4);

                var list = repo.GetByStartDate(startOfWeekTime, date);

                foreach(SparkTask task in list)
                {
                    accumulatedTime = accumulatedTime.Add(task.TimeElapsed);
                }

                if (accumulatedTime >= new TimeSpan(36, 0, 0)) 
                {
                    //activate event
                }
            }
        }
    }
}
