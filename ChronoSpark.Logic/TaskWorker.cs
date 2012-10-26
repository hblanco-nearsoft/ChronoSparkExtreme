using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Logic
{
    public class TaskWorker //trying to just receive the data needed and construct the entity might use a factory?
    {
        public IRavenEntity getItem(String newDescription, int newDuration) 
        {

            IRavenEntity workingEntity = new SparkTask 
            { Description = newDescription,
              Duration = newDuration
            };
            return workingEntity;
            
        }

    }
}
