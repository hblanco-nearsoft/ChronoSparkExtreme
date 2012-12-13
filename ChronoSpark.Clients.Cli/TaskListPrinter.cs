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
                var idNumber = parts[1];
                var timeToDisplay = sparkTask.TimeElapsed.ToString(@"hh\:mm\:ss");
                Console.WriteLine("  {0} : {1:hh\\:mm\\:ss} : {2}", idNumber, sparkTask.TimeElapsed, sparkTask.Description);
            }
            return true;
        }

    }
}
