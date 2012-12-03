using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class SetActiveTaskCmd
    {
        IRepository repo;

        public bool ActivateTask(SparkTask receivedTask) 
        {
            var activeTaskList = repo.GetActiveTask();
            TaskStateTracker stateTracker = new TaskStateTracker(activeTaskList);
            SparkTask theTask = stateTracker.VerifyOneInProgress(TaskState.InProgress);

            if (theTask == null) 
            {
                receivedTask.State = TaskState.InProgress;
                repo.Update(receivedTask);
                return true;
            }
            if (theTask != null) 
            {

            }
            return false;
        }
    }
}
