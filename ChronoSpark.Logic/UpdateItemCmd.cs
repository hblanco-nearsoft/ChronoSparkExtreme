using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoSpark.Data;
using ChronoSpark.Data.Entities;

namespace ChronoSpark.Logic
{
    class UpdateItemCmd : ICommand
    {

        Repository Repo;
       IRavenEntity ItemUpdated;

        public UpdateItemCmd() { }

        public UpdateItemCmd(Repository receivedRepository, IRavenEntity receivedItem) 
        {

            Repo = receivedRepository; 
            ItemUpdated = receivedItem;

        }

        public bool Execute()
        {

            Repo.Update(ItemUpdated);
            return true;

        }
    }
}
