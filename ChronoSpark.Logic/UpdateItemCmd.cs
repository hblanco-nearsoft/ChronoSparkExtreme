using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    public class UpdateItemCmd : ICommand, ICommandFactory  
    {

       IRepository Repo;
       IRavenEntity ItemUpdated;

        public UpdateItemCmd() { }

        public UpdateItemCmd(IRepository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository; 
            ItemUpdated = receivedItem;

        }

        public bool Execute()
        {

            Repo.Update(ItemUpdated);
            return true;

        }

        public String CommandName { get { return "update"; } }
        public String CommandDescription { get { return "update itemtosave"; } }
    }
}
