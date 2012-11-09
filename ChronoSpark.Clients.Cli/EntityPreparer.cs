using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data;

namespace ChronoSpark.Clients.Cli
{
    class EntityPreparer //this is the same as EntityWorker on the logic layer thinking where it would fit better
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
                    if (itemToAdd.Description == null || itemToAdd.Description == "") 
                    {
                        Console.WriteLine("You must add a Description");
                    }
                }

                while (itemToAdd.Duration <= 0)
                {

                    int toDuration;
                    Console.WriteLine("Add a duration (in minutes) for the task");
                    String input = Console.ReadLine();
                    if (int.TryParse(input, out toDuration))
                    {
                        itemToAdd.Duration = toDuration;
                    }              
                    if (itemToAdd.Duration <= 0)
                    {
                        Console.WriteLine("The duration must be a number greater than 0");
                    }
                }
                Console.WriteLine("Add a Client for the task");
                itemToAdd.Client = Console.ReadLine();

                itemToAdd.StartDate= DateTime.Now;
                itemToAdd.State=  TaskState.Paused;

                return itemToAdd; 
            }

            if (entityName == "reminder")
            {
                Reminder itemToAdd = new Reminder();

                while (itemToAdd.Description == null|| itemToAdd.Description == "")
                {
                    Console.WriteLine("Add a description for the reminder");
                    itemToAdd.Description = Console.ReadLine();
                }
                while (itemToAdd.Interval <= 0)
                {
                    Console.WriteLine("Add an interval (in minutes) for the reminder");
                    String input = Console.ReadLine();
                    int ToInterval;
                    if (int.TryParse(input, out ToInterval)) 
                    {
                        itemToAdd.Interval = ToInterval;
                    }
                    if (itemToAdd.Interval <= 0)
                    {
                        Console.WriteLine("The Interval must be a number greater than 0");
                    }
                }

                return itemToAdd;
            }

            return null;
        }
    }
}
