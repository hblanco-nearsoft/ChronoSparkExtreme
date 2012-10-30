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
            IRepository repo = new Repository();//trying something different
            string[] words = cmd.Split(' ');
            EntityDeterminator entityDeterminator= new EntityDeterminator();
            CommandDeterminator commandDeterminator = new CommandDeterminator();
            ICommand theCommand;
            IRavenEntity itemToWork;
            
            theCommand = commandDeterminator.GetCommand(words[0],repo);
            itemToWork = entityDeterminator.getEntity(words[1]);
            theCommand.SetEntity(itemToWork);
            SparkInvoker invoker = new SparkInvoker(theCommand);
            invoker.Invoke();
            return "The command was processed";

            //if (cmd == "add task")
            //{
            //    Console.WriteLine("Escriba una Descripcion para la tarea");
            //    string nameOfTask = Console.ReadLine();
            //    int durOfTask = 1;//for now just adding a default value will probably change later.
            //    // obviously needs to add option for other fields
            //    EntityDeterminator determinator = new EntityDeterminator();
            //    //IRavenEntity entity = determinator.getItem();
            //    //AddItemCmd command = new AddItemCmd(repo, entity);
            //    //SparkInvoker Invoker = new SparkInvoker(command);
            //    return "The command was processed:Task Added";
            //}
            //else { return cmd + " is not a recognized command"; }     

        }
    }
}
