using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Logic;
using ManyConsole;
using NDesk.Options;

namespace ChronoSpark.Clients.Cli 
{
    class ListCommand : ConsoleCommand
    {
        public ListCommand() 
        {
            this.IsCommand("list", "lists all the tasks [Id : Duration : Description]");
        }

        public override int Run(string[] remainingArguments)
        {
            SparkLogic.ReturnTaskList();
            TaskListPrinter lister = new TaskListPrinter();
            var listOfTasks = SparkLogic.ReturnTaskList();
            lister.ListTaks(listOfTasks);
            return 0;
        }
    }
}
