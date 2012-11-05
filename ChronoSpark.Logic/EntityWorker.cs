using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class EntityWorker
    {

        public IRavenEntity GetItem(String entityName)
        {

            if (entityName == "task") 
            {
                SparkTask itemToAdd = new SparkTask();
                while (itemToAdd.Description == null || itemToAdd.Description == "")
                {
                    Console.WriteLine("Add a description for the task");
                    itemToAdd.Description = Console.ReadLine();
                }

                while (itemToAdd.Duration == 0)
                {

                    int toDuration;
                    Console.WriteLine("Add a duration (in minutes) for the task");
                    String input = Console.ReadLine();
                    if (int.TryParse(input, out toDuration))
                    {
                        itemToAdd.Duration = toDuration;
                    }
                    else { Console.WriteLine("The duration must be a number"); }
                }
                Console.WriteLine("Add a Client for the task");
                itemToAdd.Client = Console.ReadLine();

                itemToAdd.StartDate= DateTime.Now;
                itemToAdd.State=  TaskState.InProgress;

                return itemToAdd; 
            }

            if (entityName == "reminder")
            {
                Reminder itemToAdd = new Reminder();

                while (itemToAdd.Description == null)
                {
                    Console.WriteLine("Add a description for the reminder");
                    itemToAdd.Description = Console.ReadLine();
                }
                while (itemToAdd.Interval == 0)
                {
                    Console.WriteLine("Add an interval (in minutes) for the reminder");
                    itemToAdd.Interval = int.Parse(Console.ReadLine());
                }

                return itemToAdd;
            }

            return null;
        }
    }
}
