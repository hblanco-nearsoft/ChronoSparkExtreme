using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChronoSpark.Logic
{
    public class SparkLogic
    {
        private static bool wasInitialized = false;
        public static bool Initialize()
        {
            if (wasInitialized) { return true; }

            Repository.RavenInitialize();
            wasInitialized = true;
            return true;
        }
        
        //private IRavenEntity ActiveTask;

        public static string ProcessCommand(string cmd)
        {

            var commandParts = cmd.Split(' ');
            IRavenEntity usableEntity;

            var availableCommands = GetAvailableCommands();
            if (cmd.Length == 0)
            {
                PrintUsage(availableCommands);
                return " ";
            }

            var parser = new CommandParser(availableCommands);
            var command = parser.ParseCommand(commandParts[0]);

            if (command == null) 
            {
                return "Unidentified Command";
            }

            if (commandParts[0] == "add")  //what happens when i receive more arguments than expected?
            {
                if (commandParts.Length < 2) 
                {
                    return "You need to specify a type of entity";
                }

                 EntityWorker entityWorker = new EntityWorker();
                 usableEntity = entityWorker.GetItem(commandParts[1]);
                 if (usableEntity == null)
                 {
                    return commandParts[1] + " is not a valid entity";
                 }
                 command.ItemToWork = usableEntity;
            }

            if (commandParts[0] == "delete") 
            {
                if (commandParts.Length < 2) 
                {
                    return "You need to specify a type of entity";
                }
                if (commandParts[1] == "task" )
                {
                    // command.ItemToWork = GetActiveTask();
                    return "";
                }
                
            }

            if (commandParts[0] == "update")
            {
                if (commandParts.Length < 2)
                {
                    return "You need to specify a type of entity";
                }
                if (commandParts[1] == "task") 
                {
                    //command.ItemToWork = GetActiveTask();
                    return "";
                }
                if (commandParts[1] == "Reminder") { }
            }

            if (commandParts[0] == "load") 
            {
                if (commandParts.Length < 2) 
                {
                    return "You need to specify a type of entity";
                }
                if (commandParts[1] == "task") 
                {
                    if (commandParts.Length < 3) { return "An ID is needed"; }
                    //command.ItemToWork = GetIdForLoading(); 
                }
                if (commandParts[1] == "reminder") 
                {
                }
            }

            if (command != null)
            {
                command.Execute();
                return "command was executed";
            }
            return "Unidentified command";
        } 

        static IEnumerable<ICommandFactory> GetAvailableCommands()
         {
             Repository repo = new Repository();
             return new ICommandFactory[]
             {
                 new AddItemCmd(repo),
                 new DeleteItemCmd(repo),
                 new UpdateItemCmd(repo),
                 new GetByIdCmd(repo),
             };
         }   

        private static void PrintUsage(IEnumerable<ICommandFactory> availableCommands)
            {
                Console.WriteLine("List of Commands:");
                foreach(var command in availableCommands)
                    Console.WriteLine("  {0}", command.CommandDescription);
            }
     

    }
}
