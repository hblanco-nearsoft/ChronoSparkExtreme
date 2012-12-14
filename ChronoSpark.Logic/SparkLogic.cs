using ChronoSpark.Data;
using ChronoSpark.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            DefaultReminders.AddDefaultReminders();
            return true;
        }

        //private IRavenEntity ActiveTask;

        //public static string ProcessCommand(ICommand cmd)
        //{

        //    if (cmd != null)
        //    {
        //        cmd.Execute();
        //        return "The command was executed";
        //    }
        //    return "Unidentified command";
        //} 

        //public static IEnumerable<ICommandFactory> GetAvailableCommands()
        // {
        //     Repository repo = new Repository();
        //     return new ICommandFactory[]
        //     {
        //         new AddItemCmd(repo),
        //         new DeleteItemCmd(repo),
        //         new UpdateItemCmd(repo),
        //         new ListTaskCmd(repo),
        //         new ListReminderCmd(repo)
        //     };
        // }

        public static IEnumerable<SparkTask> ReturnTaskList() 
        {
            Repository repo = new Repository();
            ListTaskCmd listCommand = new ListTaskCmd(repo);
            var listToReturn = listCommand.GetList();
            return listToReturn;
        }

        public static IEnumerable<Reminder> ReturnReminderList() 
        {
            Repository repo = new Repository();
            ListReminderCmd listCommand = new ListReminderCmd(repo);
            var listToReturn = listCommand.GetList();
            return listToReturn;
        }

        public SparkTask ReturnActiveTask() 
        {
            Repository repo = new Repository();
            var activeTaskToReturn = repo.GetActiveTask();
            return activeTaskToReturn;
        }

        //public static void PrintUsage(IEnumerable<ICommandFactory> availableCommands)
        //    {
        //        Console.WriteLine("List of Commands:");
        //        foreach(var command in availableCommands)
        //            Console.WriteLine("{0}", command.CommandDescription);
        //    }

        public static IRavenEntity fetch(IRavenEntity entity)
        {
            Repository repo = new Repository();
            var entityToReturn = repo.GetById(entity);
            if (entityToReturn != null) { return entityToReturn; }
            else { return null; }
        }
    }
}