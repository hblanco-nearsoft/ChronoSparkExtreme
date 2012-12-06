using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Clients.Cli
{
    public class NoActiveTaskListener
    {

        public bool Suscribe(ReminderControl reminderControl) 
        {
            reminderControl.EventNoActiveTask += new ReminderControl.ReminderHandler(NotifyLackOfActiveTask);
            return true;
        }

        public void NotifyLackOfActiveTask(object obj, ReminderEventArgs args) 
        {
            Console.WriteLine("There is no Active Task.");
            Console.WriteLine("Would you like to start one?");
            var response = Console.ReadLine();

            if (response.ToLower() == "y" || response.ToLower() == "yes") 
            {
                var taskList = SparkLogic.ReturnTaskList();
                TaskListPrinter taskPrinter = new TaskListPrinter();
                taskPrinter.ListTaks(taskList);
                Console.WriteLine("Select a Task to activate");
                var selectedTaskId = Console.ReadLine();
                SparkTask taskToActivate = new SparkTask();
                taskToActivate.Description = "sparktasks/" + selectedTaskId;
                TaskStateControl taskStateControl = new TaskStateControl();
                var result = taskStateControl.SetActiveTask(taskToActivate);
                if (result) 
                {
                    Console.WriteLine("The task has been activated");
                }
                if (!result) 
                {
                    Console.WriteLine("There already is an active task woul you like to pause it to set the new one as active?");
                    response = Console.ReadLine();
                    if (response.ToLower() == "y" || response.ToLower() == "yes") 
                    {
                        taskStateControl.PauseTask();
                        taskStateControl.SetActiveTask(taskToActivate);
                    }
                }
            }
            if (response.ToLower() == "n" || response.ToLower() == "no")
            {
                return;
            }
            else { Console.WriteLine("answer y/yes or n/no"); }
        }
    }
}
