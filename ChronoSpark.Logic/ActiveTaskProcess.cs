using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class ActiveTaskProcess
    {
        private static DateTime StartTime;
        private IRepository repo = new Repository();
        public void SetStartTime() 
        {
            StartTime = DateTime.Now;
        }
        public void AddElapsedTime() 
        {
            var activeTask = repo.GetActiveTask();
            var elapsedTime = DateTime.Now - StartTime;
            StartTime = DateTime.Now;
            activeTask.TimeElapsed = activeTask.TimeElapsed.Add(elapsedTime);
            repo.Update(activeTask);
        }
    }
}
