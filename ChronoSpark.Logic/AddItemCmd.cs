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
        IRavenEntity ItemToAdd;

        public AddItemCmd() { }

        public AddItemCmd(IRepository receivedRepo, IRavenEntity receivedItem)
        {

            ItemToAdd = receivedItem;
            Repo = receivedRepo;
                 
        }

        public bool Execute() 
        {

            Repo.Add(ItemToAdd);
            return true;
 
        }

        public String CommandName { get; set; }
        public String CommandDescription { get; set; }
    }
}
   