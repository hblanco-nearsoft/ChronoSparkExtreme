using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;

namespace ChronoSpark.Service
{
    public class SparkTaskBuilder
    {
        public SparkTask BuildTask(FormDataCollection formData) 
        {
            var drescription = formData.ElementAt(0);
            var duration = formData.ElementAt(1);
            int durationValue;

            if (!int.TryParse(duration.Value, out durationValue)) 
            {
                durationValue = 0;
            }

            var client = formData.ElementAt(2);

            SparkTask buildedTask = new SparkTask
            {
                Description = drescription.Value,
                Duration = durationValue,
                Client = client.Value,
                StartDate = DateTime.Now,
                State = TaskState.Paused,
            };

            return buildedTask;
        }

        public SparkTask RebuildTask(FormDataCollection formData) 
        {
            SparkTask taskToRetrieve = new SparkTask { Id = formData.ElementAt(3).Value };
            SparkTask retrievedTask = SparkLogic.fetch(taskToRetrieve) as SparkTask;
            String taskDescription = formData.ElementAt(0).Value;
            String taskDuration = formData.ElementAt(1).Value;
            int durationValue;                  

            if (!int.TryParse(taskDuration, out durationValue)) 
            {
                durationValue = 0;
            }

            String taskClient = formData.ElementAt(2).Value;

            retrievedTask.Description = taskDescription;
            retrievedTask.Duration = durationValue;
            retrievedTask.Client = taskClient;
            
            return retrievedTask;

        }
    }
}
