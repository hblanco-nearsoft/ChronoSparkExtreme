using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities; 

namespace ChronoSpark.Logic
{
    public class AddItemCmd : ICommand, ICommandFactory
    {
        //should the command receive the entity or just the arguments and for it inside?
        //how should i pass the repo? Should i do that in the first place?
        IRepository Repo;
        public IRavenEntity ItemToWork { set; get; }
        public AddItemCmd(IRepository receivedRepository)
        {

            Repo = receivedRepository;
                 
        }

        public bool Execute() 
        {
          
            
            Repo.Add(ItemToWork);
            Console.WriteLine("Item saved");
            return true;
        }

        public String CommandName { get { return "add"; } }
        public String CommandDescription { get { return "add task"; } }

        public ICommand MakeCommand() 
        {
            return new AddItemCmd(Repo);
        }
    }
}