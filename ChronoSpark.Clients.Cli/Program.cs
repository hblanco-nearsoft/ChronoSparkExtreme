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

namespace ChronoSpark.Clients.Cli
{
    class Program
    {
        
        static int Main(string[] args)
        {

            var commands = GetCommands(); 

            ConsoleModeCommand consoleRunner = new ConsoleModeCommand(GetCommands);
            commands = commands.Concat(new[] { consoleRunner });

            return ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);

            //var exit = false;
            //var processed = false;
            //Console.Write("Initializing ChronoSpark Time Manager...");
            //SparkLogic.Initialize();
            //Console.WriteLine("DONE!");

            //Console.WriteLine("Enter 'exit' to terminate. ");

            //while (!exit)
            //{
            //    Console.Write("ChronoSpark => ");
            //    processed = false;
            //    var cmd = Console.ReadLine();

            //    // var result = SparkLogic.ProcessCommand(cmd);
            //    if (cmd == "exit")
            //    {
            //        exit = true;
            //    }

            //    while (!processed)
            //    {
            //        var commandParts = cmd.Split(' ');
            //        IRavenEntity usableEntity;

            //        var availableCommands = SparkLogic.GetAvailableCommands();
            //        if (cmd.Length == 0)
            //        {
            //            SparkLogic.PrintUsage(availableCommands);
            //        }

            //        var parser = new CommandParser(availableCommands);
            //        var command = parser.ParseCommand(commandParts[0]);

            //        if (command == null)
            //        {
            //            Console.WriteLine("Unidentified Command");
            //            processed = true;
            //        }

            //        if (commandParts[0] == "add")
            //        {
            //            if (commandParts.Length < 2)
            //            {
            //                Console.WriteLine("You need to specify a type of entity");
            //                processed = true;
            //                break;
            //            }

            //            EntityPreparer entityPreparer = new EntityPreparer();
            //            usableEntity = entityPreparer.GetItem(commandParts[1]);
            //            if (usableEntity == null)
            //            {
            //                Console.WriteLine(commandParts[1] + " is not a valid entity");
            //                processed = true;
            //                break;
            //            }
            //            command.ItemToWork = usableEntity;
            //            var result = SparkLogic.ProcessCommand(command);
            //            Console.WriteLine(result);
            //            processed = true;
            //        }

            //        if (commandParts[0] == "list")
            //        {
            //            //var result = SparkLogic.ProcessCommand(command);
            //            //Console.WriteLine(result);
            //            TaskListPrinter lister = new TaskListPrinter();
            //            var listOfTasks = SparkLogic.ReturnList();
            //            lister.ListTaks(listOfTasks);
            //            processed = true;
            //        }
                    
            //        // exit = true;
            //    }
            //}

        }


        static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Program));
        }
    }
}
