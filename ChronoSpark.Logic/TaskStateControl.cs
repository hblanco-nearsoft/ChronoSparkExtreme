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
            if (activeTask == null || activeTask.State != TaskState.InProgress)
            {
                taskToActivate.State = TaskState.InProgress;
                taskToActivate.StartDate = DateTime.Now;
                repo.Update(taskToActivate);
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
            if (activeTask == null)
            {
                return "There is no Active Task";
            }
            else 
            {
                activeTask.State = TaskState.Paused;
                repo.Update(activeTask);
                return "The active task has been paused";
            }
        }

        public String FinishTask() 
        {
            var activeTask = repo.GetActiveTask();
            if (activeTask == null)
            {
                return "There is no Active Task";
            }
            else 
            {
                activeTask.State = TaskState.Finished;
                return "The task has been finished";
            }
        }

        public String SetAsReported()        
        {
            var taskList = SparkLogic.ReturnNotReportedList();
            if (taskList.Count() == 0) 
            {
                return "There are no tasks to report";
            }
            foreach (SparkTask task in taskList) 
            {
                task.State = TaskState.Reported;
            }
            return "The tasks have been reported";
        }
    }
}
