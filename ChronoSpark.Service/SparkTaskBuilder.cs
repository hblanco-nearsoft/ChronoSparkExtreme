using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using Omu.ValueInjecter;


namespace ChronoSpark.Service
{
    public class SparkTaskBuilder
    {
        public SparkTask BuildTask(SparkTask sparktask) 
        {          
            SparkTask builtTask = new SparkTask
            {
                Description = sparktask.Description,
                Duration = sparktask.Duration,
                Client = sparktask.Client,
                StartDate = DateTime.Now,
                State = TaskState.Paused,
            };

            return builtTask;
        }

        public SparkTask RebuildTask(SparkTask receivedTask) 
        {      
            SparkTask retrievedTask = SparkLogic.fetch(receivedTask) as SparkTask;
            retrievedTask.Description = receivedTask.Description;
            retrievedTask.Duration = receivedTask.Duration;
            retrievedTask.Client = receivedTask.Client;
            
            return retrievedTask;
        }

        public SparkTask ReturnToActivate(SparkTask taskToRetrieve)
        {
            SparkTask retrievedTask = SparkLogic.fetch(taskToRetrieve) as SparkTask;
            retrievedTask.State = TaskState.InProgress;

            return retrievedTask;

        }
    }
}
