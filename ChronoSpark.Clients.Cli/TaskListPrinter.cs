using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    class TaskListPrinter
    {
        public bool ListTaks(IEnumerable<SparkTask> receivedList) 
        {
            foreach (var sparkTask in receivedList)
            {
                var parts = sparkTask.Id.Split('/');
                var idNumber = parts.Last();
                var hours = sparkTask.TimeElapsed.TotalHours;
                var hoursInteger = (int)hours;
                var hoursToPrint = hoursInteger.ToString();
                var minutes = sparkTask.TimeElapsed.Minutes;
                var seconds = sparkTask.TimeElapsed.Seconds;
                var toDisplay = hoursToPrint + ":" + minutes + ":" + seconds;
                Console.WriteLine("  {0} : {1} : {2}", idNumber, toDisplay, sparkTask.Description);
            }
            return true;
        }

    }
}
