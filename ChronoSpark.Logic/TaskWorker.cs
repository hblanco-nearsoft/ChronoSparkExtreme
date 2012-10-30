using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;
using System.IO;

namespace ChronoSpark.Logic
{
    public class TaskWorker: IWorker
    {
        public IRavenEntity getItem() 
        {
            Console.WriteLine("Enter a Description for the Task");
            String newDescription = Console.ReadLine();

            int newDuration = 1; //kind of as default.
            IRavenEntity workingEntity = new SparkTask 
            { 
              Description = newDescription,
              Duration = newDuration
            };
            return workingEntity;
            
        }

    }
}
