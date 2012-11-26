using ChronoSpark.Logic;
using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Storage.Esent;
using Raven.Storage.Managed;
using ChronoSpark.Data;
using ManyConsole;


using System.Threading;

namespace ChronoSpark.Clients.Cli
{
    class Program
    {   
        static void Main(string[] args)
        {
            var processed = false;
            Console.Write("Initializing ChronoSpark Time Manager...");
            SparkLogic.Initialize();
            Console.WriteLine("DONE!");

            //Console.WriteLine("Enter 'exit' to terminate. ");

             


            while (!processed)//!exit
            {
                Console.Write("ChronoSpark => ");
                processed = false;
                var cmd = "run-console";

                #region debug
#if DEBUG
                SparkTask thisTask = new SparkTask 
                {
                    Description = "auto Task default premium plus",
                    Duration = 10,
                    Client = "Client",
                    StartDate = DateTime.Now,
                    State = TaskState.Paused
                };
                var availableCommands = SparkLogic.GetAvailableCommands();
                var parser = new CommandParser(availableCommands);
                var theCommand = parser.ParseCommand("add");
                theCommand.ItemToWork = thisTask;
                var result = SparkLogic.ProcessCommand(theCommand);

                Reminder thisReminder = new Reminder
                {
                    Description = "reminder 1",
                    Interval = 1
                };
                var availableCommands2 = SparkLogic.GetAvailableCommands();
                var parser2 = new CommandParser(availableCommands2);
                var theCommand2 = parser.ParseCommand("add");
                theCommand2.ItemToWork = thisReminder;
                var result2 = SparkLogic.ProcessCommand(theCommand2);

                Reminder thisReminder2 = new Reminder
                {
                    Description = "reminder 2",
                    Interval = 1
                };
                var availableCommands12 = SparkLogic.GetAvailableCommands();
                var parser12 = new CommandParser(availableCommands12);
                var theCommand12 = parser.ParseCommand("add");
                theCommand12.ItemToWork = thisReminder2; 
                var result12 = SparkLogic.ProcessCommand(theCommand12);

                //ThreadPool.QueueUserWorkItem(delegate { ReminderControl.ActivateReminder(thisReminder, thisTask); });
#endif
                #endregion  

                while (!processed)
                {
                    String[] cdmArgs = cmd.Split(' ');
                    var commands = GetCommands();
                    ConsoleModeCommand consoleRunner = new ConsoleModeCommand(GetCommands);
                    commands = commands.Concat(new[] { consoleRunner });

                    ConsoleCommandDispatcher.DispatchCommand(commands, cdmArgs, Console.Out);
                    processed = true;
                }
            }
        }

        static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}