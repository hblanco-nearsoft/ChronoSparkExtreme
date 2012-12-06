using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class TaskStateControl 
    {
        IRepository repo = new Repository();
        
        public bool SetActiveTask(SparkTask taskToActivate)
        {
            var activeTask = repo.GetActiveTask();
            if (activeTask.Count() == 0)
            {
                taskToActivate.State = TaskState.InProgress;
                return true;
            }
            else
            {
                return false;
            }
        }

        public String PauseTask() 
        {
            var activeTask = repo.GetActiveTask();
            if (activeTask.Count() == 0)
            {

                return "There is no Active Task";
            }
            else 
            {
                foreach (SparkTask t in activeTask) 
                {
                    t.State = TaskState.Paused;
                    repo.Update(t);
                }
                return "The active task has been paused";
            }

        }

        public String FinishTask() 
        {
            var activeTask = repo.GetActiveTask();
            String description;
            if (activeTask.Count() == 0)
            {

                return "There is no Active Task";
            }
            else 
            {
                return "The task has been finished";
            }


        }
    }
}
