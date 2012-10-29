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

        public IRavenEntity getItem(String entityType, String[] arguments)
        {
            if (entityType == "task")
            {
                IRavenEntity taskToReturn = new SparkTask();
                TaskWorker taskWorker = new TaskWorker();
                String description = arguments[0];
                int duration = int.Parse( arguments[1] );
                //etc
                taskToReturn = taskWorker.getTask(description, duration);
                return taskToReturn;
            }
            if (entityType == "reminder")
            {
                IRavenEntity reminderToReturn = new Reminder();
                ReminderWorker reminderWorker = new ReminderWorker();
                String description = arguments[0];
                int interval = int.Parse(arguments[1]);
                //etc
                reminderToReturn = reminderWorker.getReminder(description, interval);
                return reminderToReturn;
            }
            else
            {
                return null;
            }
        }


    }
}
