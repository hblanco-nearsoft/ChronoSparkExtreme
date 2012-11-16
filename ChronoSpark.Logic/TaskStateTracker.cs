using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class TaskStateTracker
    {

        SparkTask[] tasks;
        SparkTask TheTask;

        public TaskStateTracker(IEnumerable<SparkTask> query)
        {
            tasks = query.Cast<SparkTask>().ToArray();
        }

        private SparkTask VerifyOneInProgress(TaskState state) 
        {
            int counter = 0;
            for (var x = 0; x < tasks.Length; x++)
            {
                if (tasks[x].State == state) 
                {
                    counter++; 
                    TheTask = tasks[x];
                }
            }
            if (counter == 1) { return TheTask; }
            else { return null; }
        }

        public SparkTask this[TaskState State] 
        {
            get 
            {
                    return VerifyOneInProgress(State);
            }
        }
    }
}
