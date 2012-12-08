using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyConsole;
using NDesk.Options;
using ChronoSpark.Logic;

namespace ChronoSpark.Clients.Cli
{
    class PauseTaskCommand : ConsoleCommand
    {
        public PauseTaskCommand() 
        {
            this.IsCommand("pause", "pauses the task");
        }

        public override int Run(String[] RemainingArguments)
        {
            TaskStateControl taskStateControl = new TaskStateControl();
            SparkLogic sparkLogic = new SparkLogic();
            var activeTasks = sparkLogic.ReturnActiveTask();
            if (activeTasks.Count() == 0) 
            {
                Console.WriteLine("There are no active tasks");
                return 0;
            }
            taskStateControl.PauseTask();
            Console.WriteLine("The task has been paused.");
            return 0;
        }
    }
}
