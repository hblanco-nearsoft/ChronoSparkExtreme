using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Logic;
using ChronoSpark.Data.Entities;
using ChronoSpark.Data; 


namespace ChronoSpark.Clients.Cli
{
    class SetActiveTaskCommand : ConsoleCommand
    {
        public SetActiveTaskCommand()
        {
            this.IsCommand("activate", "sets the task to activate");
            this.HasRequiredOption("t|Task=", "The id of the task to activate", t => TaskId = t);
        }

        private String TaskId;

        public override int Run(String[] RemainingArguments)
        {
        
            IRavenEntity taskToFetch = new SparkTask();
            var actualTaskId = "SparkTasks/" + TaskId;
            taskToFetch.Id = actualTaskId;
            SparkTask taskToSet = SparkLogic.fetch(taskToFetch) as SparkTask;
            TaskStateControl taskStateControl = new TaskStateControl();
            var result = taskStateControl.SetActiveTask(taskToSet);
            if (result == true) { Console.WriteLine("The task was activated"); }
            if (taskToSet != null && result == false)
            {
                Console.WriteLine();
                taskStateControl.PauseTask();
                taskStateControl.SetActiveTask(taskToSet);
                Console.WriteLine("The Task was activate. The previous task was put on pause");
            }
            if (taskToSet == null) 
            {
                Console.WriteLine("The task specified doesn't exist");
            }
            return 0;
        }
    }
}
