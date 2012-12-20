using ChronoSpark.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Storage.Esent;
using Raven.Storage.Managed;
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

                ReminderControl defaultController = new ReminderControl();
                NoActiveTaskListener noActiveTaskListener = new NoActiveTaskListener();
                IntervalPassedListener intervalPassedListener = new IntervalPassedListener();
                TimeToReportListener timeToReportListener = new TimeToReportListener();

                noActiveTaskListener.Suscribe(defaultController);
                intervalPassedListener.Suscribe(defaultController);
                timeToReportListener.Suscribe(defaultController);
                
                ThreadPool.QueueUserWorkItem(delegate { defaultController.ActivateReminders(); });

                String[] cdmArgs = cmd.Split(' ');
                var commands = GetCommands();
                ConsoleModeCommand consoleRunner = new ConsoleModeCommand(GetCommands);
                commands = commands.Concat(new[] { consoleRunner });
                ConsoleCommandDispatcher.DispatchCommand(commands, cdmArgs, Console.Out);
                processed = true;
                
            }
        }

        static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}