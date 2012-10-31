using ChronoSpark.Data;
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

        
        public static string ProcessCommand(string cmd)
        {
           
            if(cmd.Length == 0)
            {
                var availableCommands = GetAvailableCommands();
                PrintUsage(availableCommands);
                return " "; 
            }
                return "wait";
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

            //IRepository repo = new Repository();//trying something different
            //string[] words = cmd.Split(' ');
            //EntityDeterminator entityDeterminator= new EntityDeterminator();
            //CommandDeterminator commandDeterminator = new CommandDeterminator();
            //ICommand theCommand;
            //IRavenEntity itemToWork;
            //if (words[0] != null)
            //{
            //    theCommand = commandDeterminator.GetCommand(words[0], repo);
            //    if (words[1] != null && words.Count() > 1)
            //    {
            //        itemToWork = entityDeterminator.getEntity(words[1]);
            //        if (itemToWork != null)
            //        {
            //            theCommand.SetEntity(itemToWork);
            //            SparkInvoker invoker = new SparkInvoker(theCommand);
            //            invoker.Invoke();
            //            return "The command was processed";
            //        }
            //        else { return "No such item type"; }
            //    }
            //    else { return "Please Specify the type of item to work with"; }
            //}else{ return "A command neets to be specified"; }

    }
}
