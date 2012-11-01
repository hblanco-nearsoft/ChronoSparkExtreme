using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class EntityDeterminator
    {
        public IRavenEntity getEntity(String receivedCommand)
        {
            if (receivedCommand == "task")
            {
                IRavenEntity taskToReturn = new SparkTask();
                //TaskWorker taskWorker = new TaskWorker();
                //etc
                // taskToReturn = taskWorker.getItem();
                return taskToReturn;
            }
            if (receivedCommand == "reminder")
            {
                IRavenEntity reminderToReturn = new Reminder();
                //ReminderWorker reminderWorker = new ReminderWorker();
                //etc
                //reminderToReturn = reminderWorker.getItem();
               return reminderToReturn;
            }
            else
            {
                Console.WriteLine(receivedCommand +" is not an identified entity");
                return null;
            }
        }
    }
}
