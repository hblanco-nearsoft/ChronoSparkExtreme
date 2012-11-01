using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities; 

namespace ChronoSpark.Logic
{
    public class AddTaskCmd : ICommand, ICommandFactory
    {
        //should the command receive the entity or just the arguments and for it inside?
        //how should i pass the repo? Should i do that in the first place?
        IRepository Repo;
        SparkTask ItemToAdd = new SparkTask();

        public AddTaskCmd(IRepository receivedRepository)
        {

            Repo = receivedRepository;
                 
        }

        public bool Execute() 
        {
            while (ItemToAdd.Description == null ||ItemToAdd.Description == "")
            {
                Console.WriteLine("Add a description for the task");
                ItemToAdd.Description = Console.ReadLine();
            }
            
           
            while (ItemToAdd.Duration == 0)
            {

                int toDuration;
                Console.WriteLine("Add a duration (in minutes) for the task");
                String input = Console.ReadLine();
                if (int.TryParse(input, out toDuration))
                {
                    ItemToAdd.Duration = toDuration;
                }
                else { Console.WriteLine("The duration must be a number"); }
            }
            Console.WriteLine("Add a Client for the task");
            ItemToAdd.Client = Console.ReadLine();
            Repo.Add(ItemToAdd);
            Console.WriteLine("Item saved");
            return true;
        }

        public String CommandName { get { return "add task"; } }
        public String CommandDescription { get { return "add task"; } }

        public ICommand MakeCommand() 
        {
            return new AddTaskCmd(Repo);
        }
    }
}